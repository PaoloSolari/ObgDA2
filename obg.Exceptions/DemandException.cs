using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class DemandException : Exception
    {
        public DemandException(string message) : base(message) { }

    }
}
