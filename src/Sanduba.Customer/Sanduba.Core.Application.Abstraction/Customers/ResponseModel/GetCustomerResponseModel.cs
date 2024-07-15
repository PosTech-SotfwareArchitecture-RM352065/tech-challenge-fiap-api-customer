using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Customers.ResponseModel
{
    public record CustomerResponseModel(
        Guid Id, string? Name, string? RegistryIdentification, string? Email
    );

    public record GetCustomerResponseModel(
        string Status,
        string? Message,
        ICollection<CustomerResponseModel> Customers);
}
