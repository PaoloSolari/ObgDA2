using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class PurchaseLineException : Exception
    {
        public PurchaseLineException(string message) : base(message) { }

    }
}
