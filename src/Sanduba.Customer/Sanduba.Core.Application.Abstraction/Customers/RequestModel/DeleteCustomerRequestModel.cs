using System;

namespace Sanduba.Core.Application.Abstraction.Customers.RequestModel
{
    public record DeleteCustomerRequestModel(Guid CustomerId, string Name, string Address, string PhoneNumber);
}
