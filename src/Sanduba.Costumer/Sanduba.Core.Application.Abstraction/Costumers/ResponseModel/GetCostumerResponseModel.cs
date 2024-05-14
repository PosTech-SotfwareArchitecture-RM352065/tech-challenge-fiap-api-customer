using System;

namespace Sanduba.Core.Application.Abstraction.Costumers.ResponseModel
{
    public record GetCostumerResponseModel(Guid Id, string? Name, string? RegistryIdentification, string? Email);
}
