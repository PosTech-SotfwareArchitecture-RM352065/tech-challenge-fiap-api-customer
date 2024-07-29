using Sanduba.Core.Application.Abstraction.Commons;
using Sanduba.Core.Domain.Customers;
using System;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public interface ICustomerPersistenceGateway : IAsyncPersistenceGateway<Guid, IdentifiedCustomer>
    {
        public IdentifiedCustomer? GetByLogin(string userName, string password);
        public IdentifiedCustomer? GetByIdentityNumber(CPF identityNumber);
        public Guid RequestInactivation(Guid requestId, Guid customerId, string name, string address, string phoneNumber);
    }
}