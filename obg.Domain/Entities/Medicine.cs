using obg.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace obg.Domain.Entities
{
    public class Medicine
    {
        [Key] public string Code { get; set; }
        public string Name { get; set; }
        public string SymtompsItTreats { get; set; }
        public PresentationMedicine Presentation { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public bool Prescription { get; set; }
        public bool IsActive { get; set; }

        public Medicine() { }
        public Medicine(string code, string name, string symtompsItTreats, PresentationMedicine presentation, int quantity, string unit, double price, bool prescription, bool isActive)
        {
            Code = code;
            Name = name;
            SymtompsItTreats = symtompsItTreats;
            Presentation = presentation;
            Quantity = quantity;
            Unit = unit;
            Price = price;
            Stock = 0;
            Prescription = prescription;
            IsActive = isActive;
        }

    }
}