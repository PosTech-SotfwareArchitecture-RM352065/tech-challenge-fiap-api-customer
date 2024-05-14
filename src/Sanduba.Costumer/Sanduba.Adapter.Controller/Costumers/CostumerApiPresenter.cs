using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Costumers;
using Sanduba.Core.Application.Abstraction.Costumers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Adapter.Controller.Costumers
{
    public sealed class CostumerApiPresenter : CostumerPresenter<IActionResult>
    {
        //private string SerializeToJsonString(object obj)
        //{
        //    if (obj is null) return string.Empty;

        //    try
        //    {
        //        return JsonSerializer.Serialize(obj);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public override IActionResult Present(CreateCostumerResponseModel responseModel)
        {
            if (responseModel == null) return new BadRequestObjectResult("Client not created");

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(GetCostumerResponseModel responseModel)
        {
            if (responseModel == null) return new NotFoundResult();

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(IEnumerable<GetCostumerResponseModel> responseModel)
        {
            if (responseModel == null) return new NotFoundResult();

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(LoginCostumerResponseModel responseModel)
        {
            if (responseModel == null || responseModel.Status != "Success")
            {
                return new BadRequestObjectResult(responseModel);
            }
            else
                return new OkObjectResult(responseModel);
        }
    }
}
