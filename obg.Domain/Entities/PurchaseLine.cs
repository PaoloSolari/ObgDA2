using System.ComponentModel.DataAnnotations;

namespace obg.Domain.Entities
{
    public class PurchaseLine
    {
        [Key] public string IdPurchaseLine { get; set; }
        public string MedicineCode { get; set; }
        public int MedicineQuantity { get; set; } 

        public PurchaseLine() { }
        public PurchaseLine(string idPurchaseLine, string medicineCode, int medicineQuantity)
        {
            IdPurchaseLine = idPurchaseLine;
            MedicineCode = medicineCode;
            MedicineQuantity = medicineQuantity;
        }
    }
}