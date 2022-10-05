using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace obg.Domain.Entities
{
    public class Purchase
    {
        [Key] public string IdPurchase { get; set; }
        public double Amount { get; set; }
        public string BuyerEmail { get; set; }
        public List<PurchaseLine> PurchaseLines { get; set; }
        
        public Purchase() { }
        public Purchase(string idPurchase, double amount, string buyerEmail)
        {
            IdPurchase = idPurchase;
            Amount = amount;
            BuyerEmail = buyerEmail;
            PurchaseLines = new List<PurchaseLine>();
        }

    }
}
