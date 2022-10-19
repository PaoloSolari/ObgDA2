using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace obg.Exceptions
{
    [Serializable]
    public class PetitionException : Exception
    {
        public PetitionException(string message) : base(message) { }

    }
}
