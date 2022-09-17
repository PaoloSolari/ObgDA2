namespace obg.Domain.Entities
{
    public class PurchaseLine
    {
        public string MedicineCode { get; set; }
        public int MedicineQuantity { get; set; } 

        public PurchaseLine(string medicineCode, int medicineQuantity)
        {
            MedicineCode = medicineCode;
            MedicineQuantity = medicineQuantity;
        }
    }
}