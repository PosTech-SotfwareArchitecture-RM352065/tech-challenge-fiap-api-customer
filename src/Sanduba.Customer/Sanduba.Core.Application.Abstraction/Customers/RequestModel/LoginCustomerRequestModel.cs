using System.ComponentModel.DataAnnotations;

namespace Sanduba.Core.Application.Abstraction.Customers.RequestModel
{
    public enum LoginType { Identified, Anonymous }
    public record LoginCustomerRequestModel(LoginType LoginType, string? Username = null, string? Password = null);
}
