using System;

namespace Sanduba.Core.Domain.Customers
{
    public class Request
    {
        public Guid Id { get; init; }
        public DateTime RequestedAt { get; init; }
        public RequestType Type { get; init; }
        public RequestStatus Status { get; init; }
        public string? Comments { get; init; }
    }
}
