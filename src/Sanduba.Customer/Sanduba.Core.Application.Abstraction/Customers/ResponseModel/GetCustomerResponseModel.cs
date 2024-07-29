using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Customers.ResponseModel
{
    public record CustomerResponseModel(
        Guid Id,
        string? Name,
        string? RegistryIdentification,
        string? Email,
        ICollection<CustomerRequestResponseModel> Requests
    );

    public record CustomerRequestResponseModel(
        Guid Id,
        DateTime RequestedAt,
        string Type,
        string Status,
        string Comments
    );

    public record GetCustomerResponseModel(
        string Status,
        string? Message,
        ICollection<CustomerResponseModel> Customers
    );
}
