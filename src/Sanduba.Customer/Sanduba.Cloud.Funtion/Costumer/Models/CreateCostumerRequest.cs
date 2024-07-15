namespace Sanduba.Cloud.Funtion.Customer.Models
{
    public record CreateCustomerRequest(string CPF, string Name, string Email, string Password);
}
