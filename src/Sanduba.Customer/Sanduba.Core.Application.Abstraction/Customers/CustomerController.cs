using Sanduba.Core.Application.Abstraction.Customers.RequestModel;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public abstract class CustomerController<T>
    {
        protected readonly ICustomerInteractor interactor;
        protected readonly CustomerPresenter<T> presenter;

        protected CustomerController(ICustomerInteractor interactor, CustomerPresenter<T> presenter)
        {
            this.interactor = interactor;
            this.presenter = presenter;
        }

        public abstract T CreateCustomer(CreateCustomerRequestModel requestModel);
        public abstract T GetCustomer(GetCustomerRequestModel requestModel);
        public abstract T GetAllCustomers();
        public abstract T LoginCustomer(LoginCustomerRequestModel requestModel);
        public abstract T DeleteCustomer(DeleteCustomerRequestModel requestModel);
    }
}
