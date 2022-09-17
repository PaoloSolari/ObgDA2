using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace obg.Domain.Entities
{
    public class Pharmacy
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Owner Owner { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Medicine> Medicines { get; set; }

        public Pharmacy(string name, string address, Owner owner)
        {
            Name = name;
            Address = address;
            Owner = owner;
            Employees = new List<Employee>();
            Medicines = new List<Medicine>();
        }

        public void AddEmployee(Employee employee)
        {
            this.Employees.Add(employee);
        }
    }
}
