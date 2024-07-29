using System;

namespace Sanduba.Core.Application.Abstraction.Customers.ResponseModel
{
    public record DeleteCustomerResponseModel(Guid? RequestId, string Status, string Message);
}
