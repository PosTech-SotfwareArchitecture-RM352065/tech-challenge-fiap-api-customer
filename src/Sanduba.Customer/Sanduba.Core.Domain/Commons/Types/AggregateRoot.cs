using System;

namespace Sanduba.Core.Domain.Commons.Types
{
    public abstract class AggregateRoot : Entity<Guid>
    {
        protected AggregateRoot(Guid id) : base(id) { }
    }
}
