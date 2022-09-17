using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Owner : User
    {
        public Pharmacy Pharmacy;
        public Owner(string name, string email, string password, string address, RoleUser role, string registerDate, Pharmacy pharmacy) : base(name, email, password, address, role, registerDate)
        {
            Pharmacy = pharmacy;
        }

    }
}
