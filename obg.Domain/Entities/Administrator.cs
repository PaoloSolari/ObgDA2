using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Administrator : User
    {
        public List<Pharmacy> Pharmacies { get; set; }

        public Administrator(string name, string email, string password, string address, RoleUser role, string registerDate, List<Pharmacy> pharmacies) : base (name, email, password, address, role, registerDate)
        {
            Pharmacies = pharmacies;
        }


    }
}
