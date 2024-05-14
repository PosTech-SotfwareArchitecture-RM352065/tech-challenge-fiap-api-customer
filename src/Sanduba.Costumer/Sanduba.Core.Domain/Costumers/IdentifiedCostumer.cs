using System;

namespace Sanduba.Core.Domain.Costumers
{
    public sealed class IdentifiedCostumer(Guid id) : Costumer<CPF>(id)
    {
        public static Costumer<CPF> CreateCostumer(Guid id, string registrationNumber, string name, string email, string password)
        {
            CPF registryIdentification = new CPF(registrationNumber);
            var costumer = new IdentifiedCostumer(id)
            {
                RegistryIdentification = registryIdentification,
                IdentityType = IdentityType.Identified,
                Name = name,
                Email = email,
                Password = password
            };

            costumer.ValidateEntity();

            return costumer;
        }
    }
}
