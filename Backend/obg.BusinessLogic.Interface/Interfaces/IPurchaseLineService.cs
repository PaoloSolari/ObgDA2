using obg.Domain.Entities;
using System.Collections.Generic;

namespace obg.BusinessLogic.Interface
{
    public interface IPurchaseLineService
    {
        IEnumerable<PurchaseLine> GetPurchasesLines(string token, string idPurchase);
        string UpdatePurchaseLine(string idPurchase, PurchaseLine purchaseLine);
    }
}