using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException()
        {
        }

        public UserException(string message) : base(message) { }

    }
}