using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Exceptions
{
    [Serializable]
    public class InvitationException : Exception
    {
        public InvitationException()
        {
        }

        public InvitationException(string message) : base(message) { }

    }
}
