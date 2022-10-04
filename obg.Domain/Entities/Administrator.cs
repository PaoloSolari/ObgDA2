using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Administrator : User
    {
        public Pharmacy Pharmacy { get; set; }

        public Administrator() { }
        public Administrator(string name, int code, string email, string password, string address, RoleUser role, string registerDate) : base (name, code, email, password, address, role, registerDate)
        {
            Pharmacy = new Pharmacy();
        }

    }
}
