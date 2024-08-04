using Sanduba.Core.Application.Abstraction.Customers.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Customers
{
    public interface ICustomerNotification
    { 
        public Task InactivationRequested(InactivationRequestedEvent eventData);
    }
}
