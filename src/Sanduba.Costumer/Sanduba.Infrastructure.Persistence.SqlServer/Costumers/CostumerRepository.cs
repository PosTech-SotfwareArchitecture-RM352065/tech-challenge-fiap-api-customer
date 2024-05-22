using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Sanduba.Core.Domain.Costumers;
using Sanduba.Core.Application.Abstraction.Costumers;
using System.Threading.Tasks;
using System.Threading;
using CostumerDomain = Sanduba.Core.Domain.Costumers.Costumer<Sanduba.Core.Domain.Costumers.CPF>;
using System.Data;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Costumers
{
    public class CostumerRepository(InfrastructureDbContext dbContext) : ICostumerPersistenceGateway
    {
        private readonly InfrastructureDbContext _dbContext = dbContext;

        public CostumerDomain? GetByLogin(string userName, string password)
        {
            var parameterId = new SqlParameter("@Id", Guid.Empty) { Direction = ParameterDirection.InputOutput };

            _dbContext.Database.ExecuteSqlRaw($"dbo.Sp_ValidateLogin @Username, @Password, @Id OUTPUT",
                new SqlParameter("@Username", userName),
                new SqlParameter("@Password", password),
                parameterId);

            if (parameterId.Value == DBNull.Value) return null;
            if (!Guid.TryParse(parameterId.Value.ToString(), out Guid costumerId)) return null;

            return _dbContext.Costumers
                .Where(costumer => costumer.Id == costumerId)
                .Select(item => item.ToDomain())
                .FirstOrDefault();
        }

        public CostumerDomain? GetByIdentityNumber(CPF identityNumber)
        {
            if (identityNumber is null)
                return null;

            return _dbContext.Costumers
                .Where(costumer => costumer.CPF == identityNumber.ToString())
                .Select(item => item.ToDomain())
                .FirstOrDefault();
        }

        public Task SaveAsync(CostumerDomain entity, CancellationToken cancellationToken = default)
        {
            if (entity.RegistryIdentification is null)
                throw new Exception("Clientes anônimos não são cadastrados!");

            if (_dbContext.Costumers.Where(existente => existente.CPF == entity.RegistryIdentification.ToString()).Any())
                throw new Exception("Cliente já cadastrado!");

            _dbContext.Database.ExecuteSqlRaw($"dbo.Sp_AddCostumer @Id, @CPF, @Name, @Email, @Password",
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Cpf", entity.RegistryIdentification.ToString()),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Password", entity.Password));

            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(CostumerDomain entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CostumerDomain> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CostumerDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Costumers
                .Where(costumer => costumer.Id == id)
                .Select(item => item.ToDomain())
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<CostumerDomain>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return (Task<IEnumerable<CostumerDomain>>)_dbContext.Costumers
                .Select(item => item.ToDomain())
                .AsAsyncEnumerable();
        }
    }
}
