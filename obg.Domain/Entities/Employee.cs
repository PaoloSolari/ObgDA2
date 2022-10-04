using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Employee : User
    {
        public Pharmacy Pharmacy { get; set; }

        public Employee() { }
        public Employee(string name, int code, string email, string password, string address, RoleUser role, string registerDate, Pharmacy pharmacy) : base(name, code, email, password, address, role, registerDate)
        {
            Pharmacy = pharmacy;
        }
    }
}
