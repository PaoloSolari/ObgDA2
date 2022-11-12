using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Exceptions
{
    [Serializable]
    public class ExportException : Exception
    {
        public ExportException()
        {
        }

        public ExportException(string message) : base(message) { }

    }
}