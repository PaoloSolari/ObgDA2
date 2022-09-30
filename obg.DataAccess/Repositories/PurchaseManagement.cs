using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Purchase> GetPurchases()
        {
            return ObgContext.Purchases.ToList();
        }

        public Purchase GetPurchaseById(string id)
        {
            return ObgContext.Purchases.Where<Purchase>(p => p.IdPurchase.Equals(id)).AsNoTracking().FirstOrDefault();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            ObgContext.Purchases.Attach(purchase);
            ObgContext.Entry(purchase).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeletePurchase(Purchase purchase)
        {
            ObgContext.Purchases.Remove(purchase);
            ObgContext.SaveChanges();
        }
    }
}