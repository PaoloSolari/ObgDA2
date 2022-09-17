using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Purchase
    {
        public List<PurchaseLine> PurchaseLines { get; set; }
        public double Amount { get; set; }
        public string BuyerEmail { get; set; }

        public Purchase(double amount, string buyerEmail)
        {
            PurchaseLines = new List<PurchaseLine>();
            Amount = amount;
            BuyerEmail = buyerEmail;
        }
    }
}
