using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Session
    {
        public string IdSession { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public Session(string idSession, string userName, string token)
        {
            IdSession = idSession;
            UserName = userName;
            Token = token;
        }
    }
}
