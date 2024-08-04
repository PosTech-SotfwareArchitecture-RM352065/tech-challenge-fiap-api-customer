using System;

namespace Sanduba.Core.Application.Abstraction.Customers.Events
{
    public record InactivationRequestedEvent(Guid Id, Guid CustomerId);
}
