using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IPurchaseManagement
    {
        void InsertPurchase(Purchase purchase);
        IEnumerable<Purchase> GetPurchases();
        Purchase GetPurchaseById(string id);
        void UpdatePurchase(Purchase purchase);
        void DeletePurchase(Purchase purchase);
        bool IsIdPurchaseRegistered(string idPurchase);
    }
}
