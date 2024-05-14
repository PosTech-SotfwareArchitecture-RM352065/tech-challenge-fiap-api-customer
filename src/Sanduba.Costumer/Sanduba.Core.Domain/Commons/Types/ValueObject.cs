using System;
using System.Collections.Generic;
using System.Linq;

namespace Sanduba.Core.Domain.Commons.Types
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject letf, ValueObject right)
        {
            if (ReferenceEquals(letf, null) ^ ReferenceEquals(right, null)) return false;

            return ReferenceEquals(letf, null) || letf.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject letf, ValueObject right)
        {
            return !EqualOperator(letf, right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(obj => obj != null ? obj.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject left, ValueObject right) => EqualOperator(left, right);
        public static bool operator !=(ValueObject left, ValueObject right) => NotEqualOperator(left, right);
    }
}
