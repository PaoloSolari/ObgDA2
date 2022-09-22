using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Purchase
    {
        public int IdPurchase { get; set; }
        public List<PurchaseLine> PurchaseLines { get; set; }
        public double Amount { get; set; }
        public string BuyerEmail { get; set; }

        public Purchase(int idPurchase, double amount, string buyerEmail)
        {
            IdPurchase = idPurchase;
            PurchaseLines = new List<PurchaseLine>();
            Amount = amount;
            BuyerEmail = buyerEmail;
        }
    }
}
