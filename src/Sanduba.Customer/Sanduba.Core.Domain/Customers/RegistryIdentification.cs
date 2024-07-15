using Sanduba.Core.Domain.Commons.Types;
using System;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Customers
{
    public abstract class RegistryIdentification : ValueObject
    {
        public string RegistrationName { get; init; }
        protected string IdentityNumber { get; init; }

        public RegistryIdentification(string registrationName, string identityNumber)
        {
            identityNumber = RemoveMask(identityNumber);

            if (!Validate(identityNumber))
                throw new ArgumentException("Invalid identity number", nameof(identityNumber));

            RegistrationName = registrationName;
            IdentityNumber = identityNumber;
        }

        public string ToStringWithMask() => AddMask(IdentityNumber);

        public string ToStringWithoutMask() => RemoveMask(IdentityNumber);

        public abstract bool Validate(string candidateIdentity);
        protected abstract string RemoveMask(string identityNumber);
        protected abstract string AddMask(string identityNumber);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdentityNumber;
        }
    }
}
