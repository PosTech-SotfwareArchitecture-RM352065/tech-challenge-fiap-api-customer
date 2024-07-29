using System;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Customers
{
    public sealed class IdentifiedCustomer: Customer<CPF>
    {
        public IdentifiedCustomer(Guid id, string registrationNumber, string name, string email, string password)
            : base(id)
        {
            CPF registryIdentification = new CPF(registrationNumber);
            RegistryIdentification = registryIdentification;
            IdentityType = IdentityType.Identified;
            Name = name;
            Email = email;
            Password = password;
        }

        public static Customer<CPF> CreateCustomer(Guid id, string registrationNumber, string name, string email, string password)
        {
            IdentifiedCustomer customer = new 
            (
                id,
                registrationNumber,
                name,
                email,
                password
            );

            customer.ValidateEntity();

            return customer;
        }
    }
}
