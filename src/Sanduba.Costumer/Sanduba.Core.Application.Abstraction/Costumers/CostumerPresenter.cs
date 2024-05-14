using Sanduba.Core.Application.Abstraction.Costumers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Costumers
{
    public abstract class CostumerPresenter<T>
    {
        public abstract T Present(CreateCostumerResponseModel responseModel);
        public abstract T Present(GetCostumerResponseModel responseModel);
        public abstract T Present(IEnumerable<GetCostumerResponseModel> responseModel);
        public abstract T Present(LoginCostumerResponseModel repsonseModel);
    }
}
