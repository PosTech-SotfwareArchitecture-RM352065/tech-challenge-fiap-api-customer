﻿using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Sanduba.Core.Domain.Customers;
using Sanduba.Core.Application.Abstraction.Customers;
using System.Threading.Tasks;
using System.Threading;
using IdentifiedCustomer = Sanduba.Core.Domain.Customers.IdentifiedCustomer;
using AutoMapper;

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

            var query = _dbContext.Customers
                .Where(customer => customer.Id == customerId)
                .FirstOrDefault();

            return _mapper.Map<IdentifiedCustomer>(query);
        }

        public IdentifiedCustomer? GetByIdentityNumber(CPF identityNumber)
        {
            if (identityNumber is null)
                return null;

            var query = _dbContext.Customers
                .Include(customer => customer.Requests)
                .Where(customer => customer.CPF == identityNumber.ToString())
                .FirstOrDefault();

            return _mapper.Map<IdentifiedCustomer>(query);
        }

        public Guid RequestInactivation(Guid requestId, Guid customerId, string name, string address, string phoneNumber)
        {
            var request = new Schemas.CustomerRequest()
            {
                Id = requestId,
                CustomerId = customerId,
                RequestedAt = DateTime.UtcNow,
                Status = "Requested",
                Type = "Delete",
                Comments = $"Requested by Name: {name} Address: {address} PhoneNumber: {phoneNumber}"
            };

            _dbContext.CustomerRequests.AddAsync(request);
            _dbContext.SaveChanges();

            return requestId;
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

        public async Task<IdentifiedCustomer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await _dbContext.Customers
                .Include(customer => customer.Requests)
                .Where(customer => customer.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<IdentifiedCustomer>(query);
        }

        public async Task<List<IdentifiedCustomer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = await _dbContext.Customers
                .Include(customer => customer.Requests)
                .ToListAsync();

            return _mapper.Map<List<IdentifiedCustomer>>(query);
        }

        public void UpdateRequest(Guid requestId, RequestStatus status)
        {
            _dbContext.CustomerRequests
                .Where(request => request.Id == requestId)
                .ExecuteUpdate(request => request
                    .SetProperty(o => o.Status, status.ToString())
                );

            var request = _dbContext.CustomerRequests
                .Where(request => request.Id == requestId)
                .FirstOrDefault();

            _dbContext.Customers
                .Where(customer => customer.Id == request.CustomerId)
                .ExecuteUpdate(customer => customer
                    .SetProperty(o => o.Name, o => null)
                    .SetProperty(o => o.CPF, o => null)
                    .SetProperty(o => o.Email, o => null)
                    .SetProperty(o => o.Password, o => null)
                    .SetProperty(o => o.Active, false)
                );

            _dbContext.SaveChanges();
        }
    }
}
