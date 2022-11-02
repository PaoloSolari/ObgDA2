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
        public List<Medicine> Medicines { get; set; }
        public List<Demand> Demands { get; set; }
        public List<Purchase> Purchases { get; set; }

        public Pharmacy() { }
        public Pharmacy(string name, string address)
        {
            Name = name;
            Address = address;
            Medicines = new List<Medicine>();
            Demands = new List<Demand>();
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
            return this.Name == pharmacy.Name;
        }

    }
}
