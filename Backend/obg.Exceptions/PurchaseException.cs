using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class PurchaseException : Exception
    {
        public PurchaseException()
        {
        }

        public PurchaseException(string message) : base(message) { }

    }
}
