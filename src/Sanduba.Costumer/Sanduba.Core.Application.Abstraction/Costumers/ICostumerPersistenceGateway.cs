using Sanduba.Core.Application.Abstraction.Commons;
using Sanduba.Core.Domain.Costumers;
using System;

namespace Sanduba.Core.Application.Abstraction.Costumers
{
    public interface ICostumerPersistenceGateway : IAsyncPersistenceGateway<Guid, Costumer<CPF>>
    {
        public Costumer<CPF>? GetByLogin(string userName, string password);

        public Costumer<CPF>? GetByIdentityNumber(CPF identityNumber);
    }
}
