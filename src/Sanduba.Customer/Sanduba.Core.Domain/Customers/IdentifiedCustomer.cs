using System;

namespace Sanduba.Core.Domain.Customers
{
    public sealed class IdentifiedCustomer(Guid id) : Customer<CPF>(id)
    {
        public static Customer<CPF> CreateCustomer(Guid id, string registrationNumber, string name, string email, string password)
        {
            CPF registryIdentification = new CPF(registrationNumber);
            var customer = new IdentifiedCustomer(id)
            {
                RegistryIdentification = registryIdentification,
                IdentityType = IdentityType.Identified,
                Name = name,
                Email = email,
                Password = password
            };

            customer.ValidateEntity();

            return customer;
        }
    }
}
