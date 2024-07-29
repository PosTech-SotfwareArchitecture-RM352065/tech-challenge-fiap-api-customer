using System;

namespace Sanduba.Cloud.Funtion.Customer.Models
{
    public record DeleteCustomerRequest(Guid CustomerId, string Name, string Address, string PhoneNumber);
}
