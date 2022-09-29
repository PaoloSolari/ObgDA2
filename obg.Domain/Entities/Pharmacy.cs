using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace obg.Domain.Entities
{
    public class Pharmacy
    {
        [Key] public string Name { get; set; }
        public string Address { get; set; }
        public Owner Owner { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Medicine> Medicines { get; set; }

        public Pharmacy() { }
        public Pharmacy(string name, string address, Owner owner, List<Employee> employees, List<Medicine> medicines)
        {
            Name = name;
            Address = address;
            Owner = owner;
            Employees = employees;
            Medicines = medicines;
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}
