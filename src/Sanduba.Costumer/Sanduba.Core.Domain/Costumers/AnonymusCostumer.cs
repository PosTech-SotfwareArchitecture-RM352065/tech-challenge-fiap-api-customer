using System;

namespace Sanduba.Core.Domain.Costumers
{
    public sealed class AnonymusCostumer(Guid id) : Costumer<CPF>(id)
    {
        public static Costumer<CPF> CreateCostumer(Guid id)
        {
            var costumer = new AnonymusCostumer(id)
            {
                RegistryIdentification = null,
                IdentityType = IdentityType.Anonymus,
                Name = string.Empty,
                Email = string.Empty
            };

            costumer.ValidateEntity();

            return costumer;
        }
    }
}
