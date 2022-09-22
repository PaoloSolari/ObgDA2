namespace obg.Domain.Entities
{
    public class PurchaseLine
    {
        public int IdPurchaseLine { get; set; }
        public string MedicineCode { get; set; }
        public int MedicineQuantity { get; set; } 

        public PurchaseLine(int idPurchaseLine, string medicineCode, int medicineQuantity)
        {
            IdPurchaseLine = idPurchaseLine;
            MedicineCode = medicineCode;
            MedicineQuantity = medicineQuantity;
        }
    }
}