using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;
using Sanduba.Core.Application.Abstraction.Costumers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Costumers
{
    public interface ICostumerInteractor
    {
        public CreateCostumerResponseModel CreateCostumer(CreateCostumerRequestModel requestModel);
        public GetCostumerResponseModel GetCostumer(GetCostumerRequestModel requestModel);
        public IEnumerable<GetCostumerResponseModel> GetAllCostumer();
        public LoginCostumerResponseModel LoginCostumer(LoginCostumerRequestModel requestModel);
    }
}
