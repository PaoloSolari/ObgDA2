using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace obg.Domain.Entities
{
    public class Pharmacy
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public Owner Owner { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Medicine> Medicines { get; set; }

        public Pharmacy(string name, string address, Owner owner, List<Employee> employees, List<Medicine> medicines)
        {
            Name = name;
            Address = address;
            Owner = owner;
            Employees = employees;
            Medicines = medicines;
        }

        public Pharmacy()
        {

        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Pharmacy pharmacy = obj as Pharmacy;
            if (pharmacy == null)
            {
                return false;
            }
            return this.Id == pharmacy.Id;
        }
    }
}
