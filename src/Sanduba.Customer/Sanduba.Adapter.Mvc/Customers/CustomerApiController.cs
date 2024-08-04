using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.RequestModel;

namespace Sanduba.Adapter.Mvc.Customers
{
    public sealed class CustomerApiController : CustomerController<IActionResult>
    {
        public CustomerApiController(ICustomerInteractor interactor, CustomerPresenter<IActionResult> presenter) : base(interactor, presenter) { }

        public override IActionResult CreateCustomer(CreateCustomerRequestModel requestModel)
        {
            var responseModel = interactor.CreateCustomer(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetCustomer(GetCustomerRequestModel requestModel)
        {
            var responseModel = interactor.GetCustomer(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetAllCustomers()
        {
            var responseModel = interactor.GetAllCustomers();
            return presenter.Present(responseModel);
        }

        public override IActionResult LoginCustomer(LoginCustomerRequestModel requestModel)
        {
            var responseModel = interactor.LoginCustomer(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult DeleteCustomer(DeleteCustomerRequestModel requestModel)
        {
            var responseModel = interactor.DeleteCustomer(requestModel);
            return presenter.Present(responseModel);
        }
    }
}
