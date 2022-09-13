using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Administrator : User
    {
        public List<Pharmacy> Pharmacies;

        public Administrator(string name, string code, string email, string password, string address, RoleUser role, string registerDate, List<Pharmacy> pharmacies) : base (name, code, email, password, address, role, registerDate)
        {
            Pharmacies = pharmacies;
        }


    }
}
