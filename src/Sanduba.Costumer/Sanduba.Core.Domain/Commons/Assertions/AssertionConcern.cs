using Sanduba.Core.Domain.Commons.Exceptions;
using System;
using System.Collections;

namespace Sanduba.Core.Domain.Commons.Assertions
{
    public static class AssertionConcern
    {
        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            AssertArgumentLength(stringValue, 0, maximum, message);
        }

        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (string.IsNullOrEmpty(stringValue) || string.IsNullOrWhiteSpace(stringValue))
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentNotNegative(double value, string message)
        {
            if (value < 0.0)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentNotEmpty(ICollection collection, string message)
        {
            if (collection.Count < 1)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentEqual(Enum left, Enum right, string message)
        {
            if (left != right)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentNotEqual(Enum left, Enum right, string message)
        {
            if (left == right)
            {
                throw new DomainException(message);
            }
        }
    }
}
