namespace Sanduba.Core.Application.Abstraction.Costumers.RequestModel
{
    public record CreateCostumerRequestModel(string CPF, string Name, string Email, string Password);
}
