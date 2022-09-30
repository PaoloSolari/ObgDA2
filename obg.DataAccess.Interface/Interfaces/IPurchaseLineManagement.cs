using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IPurchaseLineManagement
    {
        void InsertPurchaseLine(PurchaseLine purchaseLine);
        IEnumerable<PurchaseLine> GetPurchaseLines();
        PurchaseLine GetPurchaseLineById(string id);
        void UpdatePurchaseLine(PurchaseLine purchaseLine);
        void DeletePurchaseLine(PurchaseLine purchaseLine);
        bool IsIdPurchaseLineRegistered(string idPurchaseLine);
    }
}
