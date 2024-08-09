using Sanduba.Core.Domain.Commons.Types;
using System;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Customers
{
    public abstract class RegistryIdentification : ValueObject
    {
        public string RegistrationName { get; init; }
        protected string? IdentityNumber { get; init; }

        public RegistryIdentification(string registrationName, string identityNumber)
        {
            identityNumber = RemoveMask(identityNumber);

            RegistrationName = registrationName;
            IdentityNumber = identityNumber;
        }

        public string ToStringWithMask() => AddMask(IdentityNumber);

        public string ToStringWithoutMask() => RemoveMask(IdentityNumber);

        protected abstract string RemoveMask(string identityNumber);
        protected abstract string AddMask(string identityNumber);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdentityNumber;
        }
    }
}
