using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Sanduba.Core.Domain.Customers;
using Sanduba.Core.Application.Abstraction.Customers;
using System.Threading.Tasks;
using System.Threading;
using IdentifiedCustomer = Sanduba.Core.Domain.Customers.Customer<Sanduba.Core.Domain.Customers.CPF>;
using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Customers
{
    public class CustomerRepository(InfrastructureDbContext dbContext, IMapper mapper) : ICustomerPersistenceGateway
    {
        private readonly InfrastructureDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public IdentifiedCustomer? GetByLogin(string userName, string password)
        {
            var parameterId = new SqlParameter("@Id", Guid.Empty) { Direction = ParameterDirection.InputOutput };

            _dbContext.Database.ExecuteSqlRaw($"dbo.Sp_ValidateLogin @Username, @Password, @Id OUTPUT",
                new SqlParameter("@Username", userName),
                new SqlParameter("@Password", password),
                parameterId);

            if (parameterId.Value == DBNull.Value) return null;
            if (!Guid.TryParse(parameterId.Value.ToString(), out Guid customerId)) return null;

            return _dbContext.Customers
                .Where(customer => customer.Id == customerId)
                .ProjectTo<IdentifiedCustomer>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public IdentifiedCustomer? GetByIdentityNumber(CPF identityNumber)
        {
            if (identityNumber is null)
                return null;

            var query = _dbContext.Customers
                .Where(customer => customer.CPF == identityNumber.ToString())
                .FirstOrDefault();

            return _mapper.Map<IdentifiedCustomer>(query);
        }

        public Task SaveAsync(IdentifiedCustomer entity, CancellationToken cancellationToken = default)
        {
            if (entity.RegistryIdentification is null)
                throw new Exception("Clientes anônimos não são cadastrados!");

            if (_dbContext.Customers.Where(existente => existente.CPF == entity.RegistryIdentification.ToString()).Any())
                throw new Exception("Cliente já cadastrado!");

            _dbContext.Database.ExecuteSqlRaw($"dbo.Sp_AddCustomer @Id, @CPF, @Name, @Email, @Password",
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Cpf", entity.RegistryIdentification.ToString()),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Password", entity.Password));

            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(IdentifiedCustomer entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IdentifiedCustomer> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IdentifiedCustomer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Customers
                .Where(customer => customer.Id == id)
                .ProjectTo<IdentifiedCustomer>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<IdentifiedCustomer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = await _dbContext.Customers
                .ToListAsync();

            return _mapper.Map<List<IdentifiedCustomer>>(query);
        }
    }
}
