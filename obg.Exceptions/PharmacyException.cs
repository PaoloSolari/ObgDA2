using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class PharmacyException : Exception
    {
        public PharmacyException(string message) : base(message) { }

    }
}
