using System;

namespace Sanduba.Core.Domain.Commons.Events
{
    public record DomainEvent
    {
        public DateTimeOffset OccurredAt { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
