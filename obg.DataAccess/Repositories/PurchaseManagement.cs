using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;

namespace obg.DataAccess.Repositories
{
    public class PurchaseManagement : IPurchaseManagement
    {
        private ObgContext ObgContext { get; set; }
        public PurchaseManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertPurchase(Purchase purchase)
        {
            ObgContext.Purchases.Add(purchase);
            ObgContext.SaveChanges();
        }
    }
}