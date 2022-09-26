using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseService : IPurchaseService
    {
        protected List<Purchase> fakeDB = new List<Purchase>();
        //private readonly IPurchaseManagement _purchaseManagement;

        //public PurchaseService(IPurchaseManagement purchaseManagement)
        //{
        //    _purchaseManagement = purchaseManagement;
        //}

        public Purchase InsertPurchase(Purchase purchase)
        {
            if (IsPurchaseValid(purchase))// && !IsCodeRegistered(medicine.Name))
            {
                // Se agreaga la Purchase a la DB: _purchaseManagement.InsertPurchase(purchase);
                fakeDB.Add(purchase);
            }
            return purchase;
        }

        private bool IsPurchaseValid(Purchase purchase)
        {
            if (purchase == null) throw new PurchaseException("Compra inválida.");
            if (purchase.PurchaseLines == null || purchase.PurchaseLines.Count == 0) throw new PurchaseException("Compra inválida.");
            if (purchase.Amount < 0) throw new PurchaseException("Monto inválido.");
            if (purchase.BuyerEmail == null || purchase.BuyerEmail.Length < 1) throw new PurchaseException("Email inválido.");

            return true;
        }

        public IEnumerable<Purchase> GetPurchases()
        {
            return fakeDB;
        }
    }
}
