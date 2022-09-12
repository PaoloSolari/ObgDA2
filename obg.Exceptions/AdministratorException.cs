using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class AdministratorException : Exception
    {
        public AdministratorException()
        {
        }

        public AdministratorException(string message) : base(message)
        {
        }

        public AdministratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdministratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}