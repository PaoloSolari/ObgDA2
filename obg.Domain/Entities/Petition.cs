using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Petition
    {
        public string MedicineCode { get; set; }
        public int NewQuantity { get; set; }
        public Petition(string medicineCode, int newQuantity)
        {
            MedicineCode = medicineCode;
            this.NewQuantity = newQuantity;
        }
        public Petition()
        {

        }
    }
}
