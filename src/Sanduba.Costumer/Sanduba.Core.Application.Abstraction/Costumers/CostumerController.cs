using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;

namespace Sanduba.Core.Application.Abstraction.Costumers
{
    public abstract class CostumerController<T>
    {
        protected readonly ICostumerInteractor interactor;
        protected readonly CostumerPresenter<T> presenter;

        protected CostumerController(ICostumerInteractor interactor, CostumerPresenter<T> presenter)
        {
            this.interactor = interactor;
            this.presenter = presenter;
        }

        public abstract T CreateCostumer(CreateCostumerRequestModel requestModel);
        public abstract T GetCostumer(GetCostumerRequestModel requestModel);
        public abstract T GetAllCostumers();
        public abstract T LoginCostumer(LoginCostumerRequestModel requestModel);
    }
}
