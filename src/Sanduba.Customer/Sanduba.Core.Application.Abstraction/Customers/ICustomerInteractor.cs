using Sanduba.Core.Application.Abstraction.Customers.RequestModel;
using Sanduba.Core.Application.Abstraction.Customers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public interface ICustomerInteractor
    {
        public CreateCustomerResponseModel CreateCustomer(CreateCustomerRequestModel requestModel);
        public GetCustomerResponseModel GetCustomer(GetCustomerRequestModel requestModel);
        public GetCustomerResponseModel GetAllCustomers();
        public LoginCustomerResponseModel LoginCustomer(LoginCustomerRequestModel requestModel);
    }
}
