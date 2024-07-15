namespace Sanduba.Core.Application.Abstraction.Customers.RequestModel
{
    public record CreateCustomerRequestModel(string CPF, string Name, string Email, string Password);
}
