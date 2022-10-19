using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace obg.Domain.Entities
{
    public class Session
    {
        [Key] public string IdSession { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public Session() { }
        public Session(string idSession, string userName, string token)
        {
            IdSession = idSession;
            UserName = userName;
            Token = token;
        }

    }
}
