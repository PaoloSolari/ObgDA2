using obg.Domain.Enums;
using System;

namespace obg.Domain.Entities
{
    public class Medicine
    {
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string SymtompsItTreats { get; set; }
        public PresentationMedicine Presentation { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public bool Prescription { get; set; }
        public bool isActive { get; set; }

        public Medicine(string name, string symtompsItTreats, PresentationMedicine presentation, int quantity, string unit, double price, bool prescription, bool isActive)
        {
            Code = new Guid();
            Name = name;
            SymtompsItTreats = symtompsItTreats;
            Presentation = presentation;
            Quantity = quantity;
            Unit = unit;
            Price = price;
            Prescription = prescription;
            this.isActive = isActive;
        }
    }
}