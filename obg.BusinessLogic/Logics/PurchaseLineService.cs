using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseLineService
    {
        protected List<PurchaseLine> fakeDB = new List<PurchaseLine>();
        private readonly IPurchaseLineManagement _purchaseLineManagement;

        public PurchaseLineService(IPurchaseLineManagement purchaseLineManagement)
        {
            _purchaseLineManagement = purchaseLineManagement;
        }

        public void InsertPurchaseLine(PurchaseLine purchaseLine)
        {
            if (IsPurchaseLineValid(purchaseLine))// && !IsCodeRegistered(medicine.Name))
            {
                // Se agreaga la PurchaseLine a la DB: _purchaseLineManagement.InsertMedicine(purchaseLine);
                fakeDB.Add(purchaseLine);
            }
        }

        private bool IsPurchaseLineValid(PurchaseLine purchaseLine)
        {
            if (purchaseLine == null) throw new PurchaseLineException("Linea de compra inválida.");
            if (purchaseLine.MedicineCode == null || purchaseLine.MedicineCode.Length < 1) throw new PurchaseLineException("Código de medicamento inválido");
            if (purchaseLine.MedicineQuantity < 1) throw new PurchaseLineException("La cantidad del medicamento a comprar no puede ser menor a 1");
            return true;
        }
    }
}
