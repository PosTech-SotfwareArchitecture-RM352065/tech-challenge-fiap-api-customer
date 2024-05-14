using System.ComponentModel.DataAnnotations;

namespace Sanduba.Core.Application.Abstraction.Costumers.RequestModel
{
    public enum LoginType { Identified, Anonymous }
    public record LoginCostumerRequestModel(LoginType LoginType, string? Username = null, string? Password = null);
}
