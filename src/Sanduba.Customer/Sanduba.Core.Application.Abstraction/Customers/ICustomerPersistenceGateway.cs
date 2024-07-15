using Sanduba.Core.Application.Abstraction.Commons;
using Sanduba.Core.Domain.Customers;
using System;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public interface ICustomerPersistenceGateway : IAsyncPersistenceGateway<Guid, Customer<CPF>>
    {
        public Customer<CPF>? GetByLogin(string userName, string password);

        public Customer<CPF>? GetByIdentityNumber(CPF identityNumber);
    }
}
