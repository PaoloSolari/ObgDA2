using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetPurchases(string token);
        string InsertPurchase(Purchase purchase);
        Purchase UpdatePurchase(string idPurchase, Purchase purchase, string token);
    }
}
