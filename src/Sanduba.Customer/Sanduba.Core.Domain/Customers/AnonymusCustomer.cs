using System;

namespace Sanduba.Core.Domain.Customers
{
    public sealed class AnonymusCustomer(Guid id) : Customer<CPF>(id)
    {
        public static Customer<CPF> CreateCustomer(Guid id)
        {
            var customer = new AnonymusCustomer(id)
            {
                RegistryIdentification = null,
                IdentityType = IdentityType.Anonymus,
                Name = string.Empty,
                Email = string.Empty
            };

            customer.ValidateEntity();

            return customer;
        }
    }
}
