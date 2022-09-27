using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }

    }
}
