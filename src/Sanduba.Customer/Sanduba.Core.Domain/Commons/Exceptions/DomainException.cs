using System;

namespace Sanduba.Core.Domain.Commons.Exceptions
{
    public class DomainException : Exception
    {
        private DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
