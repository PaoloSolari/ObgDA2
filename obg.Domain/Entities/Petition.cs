using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Petition
    {
        public string IdPetition { get; set; }
        public string MedicineCode { get; set; }
        public int NewQuantity { get; set; }
        public Petition(string idPetition, string medicineCode, int newQuantity)
        {
            IdPetition = idPetition;
            MedicineCode = medicineCode;
            NewQuantity = newQuantity;
        }
        public Petition()
        {

        }
    }
}
