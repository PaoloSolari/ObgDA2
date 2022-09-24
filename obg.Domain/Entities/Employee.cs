using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Employee : User
    {
        public Pharmacy Pharmacy;
        public List<Demand> Demands;

        public Employee(string name, int code, string email, string password, string address, RoleUser role, string registerDate, Pharmacy pharmacy, List<Demand> demands) : base(name, code, email, password, address, role, registerDate)
        {
            Pharmacy = pharmacy;
            Demands = demands;
        }
    }
}
