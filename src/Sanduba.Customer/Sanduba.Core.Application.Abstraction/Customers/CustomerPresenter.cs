using Sanduba.Core.Application.Abstraction.Customers.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public abstract class CustomerPresenter<T>
    {
        public abstract T Present(CreateCustomerResponseModel responseModel);
        public abstract T Present(GetCustomerResponseModel responseModel);
        public abstract T Present(IEnumerable<GetCustomerResponseModel> responseModel);
        public abstract T Present(LoginCustomerResponseModel repsonseModel);
        public abstract T Present(DeleteCustomerResponseModel repsonseModel);
    }
}
