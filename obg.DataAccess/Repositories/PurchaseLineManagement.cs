using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class PurchaseLineManagement : IPurchaseLineManagement
    {
        private ObgContext ObgContext { get; set; }
        public PurchaseLineManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertPurchaseLine(PurchaseLine purchaseLine)
        {
            ObgContext.PurchaseLines.Add(purchaseLine);
            ObgContext.SaveChanges();
        }

        public IEnumerable<PurchaseLine> GetPurchaseLines()
        {
            return ObgContext.PurchaseLines.ToList();
        }

        public PurchaseLine GetPurchaseLineById(string id)
        {
            return ObgContext.PurchaseLines.Where<PurchaseLine>(pl => pl.IdPurchaseLine.Equals(id)).AsNoTracking().FirstOrDefault();
        }

        public void UpdatePurchaseLine(PurchaseLine purchaseLine)
        {
            ObgContext.PurchaseLines.Attach(purchaseLine);
            ObgContext.Entry(purchaseLine).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeletePurchaseLine(PurchaseLine purchaseLine)
        {
            ObgContext.PurchaseLines.Remove(purchaseLine);
            ObgContext.SaveChanges();
        }

        public bool IsIdPurchaseLineRegistered(string idPurchaseLine)
        {
            PurchaseLine purchaseLine = ObgContext.PurchaseLines.Where<PurchaseLine>(pl => pl.IdPurchaseLine.Equals(idPurchaseLine)).AsNoTracking().FirstOrDefault();
            if(purchaseLine != null)
            {
                return true;
            }
            return false;
        }

        public bool IsMedicineCodeOk
    }
}
