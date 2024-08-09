using Sanduba.Core.Domain.Customers;
using System;

namespace Sanduba.Core.Application.Abstraction.Customers.Events
{
    public record InactivationRequestCompletedEvent(
        Guid Id,
        RequestStatus Status,
        DateTime CompletedAt
    );
}
