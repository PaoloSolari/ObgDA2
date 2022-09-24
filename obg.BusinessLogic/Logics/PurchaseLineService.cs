﻿using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseLineService
    {
        private readonly IPurchaseLineManagement _purchaseLineManagement;

        public PurchaseLineService(IPurchaseLineManagement purchaseLineManagement)
        {
            _purchaseLineManagement = purchaseLineManagement;
        }

        public void InsertPurchaseLine(PurchaseLine purchaseLine)
        {
            if (IsPurchaseLineValid(purchaseLine))// && !IsCodeRegistered(medicine.Name))
            {
                // Se agrega la PurchaseLine a la DB: _purchaseLineManagement.InsertMedicine(purchaseLine);
                FakeDB.PurchaseLines.Add(purchaseLine);
            }
        }

        private bool IsPurchaseLineValid(PurchaseLine purchaseLine)
        {
            if (purchaseLine == null)
            {
                throw new PurchaseLineException("Linea de compra inválida.");
            }
            if (purchaseLine.IdPurchaseLine == null || purchaseLine.IdPurchaseLine.Length < 1)
            {
                throw new PurchaseLineException("Identificador inválido.");
            }
            if (IsIdPurchaseLineRegistered(purchaseLine.IdPurchaseLine))
            {
                throw new PurchaseLineException("Ya existe una línea de compra con el mismo identificador");
            }
            if (purchaseLine.MedicineCode == null || purchaseLine.MedicineCode.Length == 0)
            {
                throw new PurchaseLineException("Código inválido.");
            }
            if (!IsMedicineCodeOk(purchaseLine.MedicineCode))
            {
                throw new PurchaseLineException("El medicamento no existe.");
            }
            if (purchaseLine.MedicineQuantity < 1)
            {
                throw new PurchaseLineException("La cantidad del medicamento a comprar no puede ser menor a 1");
            }
            return true;
        }

        public bool IsIdPurchaseLineRegistered(string idPurchaseLine)
        {
            foreach (PurchaseLine purchaseLine in FakeDB.PurchaseLines)
            {
                if (purchaseLine.IdPurchaseLine.Equals(idPurchaseLine))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsMedicineCodeOk(string medicineCode)
        {
            // Se chequea que la medicina de la compra exista en la DB.
            foreach (Medicine medicine in FakeDB.Medicines)
            {
                if (medicine.Code.Equals(medicineCode))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
