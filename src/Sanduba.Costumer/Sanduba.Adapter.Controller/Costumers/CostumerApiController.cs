using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Costumers;
using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;

namespace Sanduba.Adapter.Controller.Costumers
{
    public sealed class CostumerApiController : CostumerController<IActionResult>
    {
        public CostumerApiController(ICostumerInteractor interactor, CostumerPresenter<IActionResult> presenter) : base(interactor, presenter) { }

        public override IActionResult CreateCostumer(CreateCostumerRequestModel requestModel)
        {
            var responseModel = interactor.CreateCostumer(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetCostumer(GetCostumerRequestModel requestModel)
        {
            var responseModel = interactor.GetCostumer(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetAllCostumers()
        {
            var responseModel = interactor.GetAllCostumer();
            return presenter.Present(responseModel);
        }

        public override IActionResult LoginCostumer(LoginCostumerRequestModel requestModel)
        {
            var responseModel = interactor.LoginCostumer(requestModel);
            return presenter.Present(responseModel);
        }
    }
}
