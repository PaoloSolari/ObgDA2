using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class SessionException : Exception
    {
        public SessionException(string message) : base(message) { }

    }
}