using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Adapter.Mvc.Customers
{
    public sealed class CustomerApiPresenter : CustomerPresenter<IActionResult>
    {
        public override IActionResult Present(CreateCustomerResponseModel responseModel)
        {
            if (responseModel == null) return new BadRequestObjectResult("Client not created");

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(GetCustomerResponseModel responseModel)
        {
            if (responseModel == null) return new NotFoundResult();

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(IEnumerable<GetCustomerResponseModel> responseModel)
        {
            if (responseModel == null) return new NotFoundResult();

            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(LoginCustomerResponseModel responseModel)
        {
            if (responseModel == null || responseModel.Status != "Success")
            {
                return new BadRequestObjectResult(responseModel);
            }
            else
                return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(DeleteCustomerResponseModel responseModel)
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
