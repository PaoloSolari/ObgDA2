using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseManagement _purchaseManagement;
        private readonly IMedicineManagement _medicineManagement;
        private readonly IPharmacyManagement _pharmacyManagement;

        public PurchaseService(IPurchaseManagement purchaseManagement, IMedicineManagement medicineManagement, IPharmacyManagement pharmacyManagement)
        {
            _purchaseManagement = purchaseManagement;
            _medicineManagement = medicineManagement;
            _pharmacyManagement = pharmacyManagement;
        }

        public string InsertPurchase(Purchase purchase)
        {
            purchase.IdPurchase = CreateId();
            SetIdsLinesOfPurchase(purchase.PurchaseLines);
            //if (MedicinesOfTheSamePharmacy(purchase.PurchaseLines))
            //{
                if (ThereIsStock(purchase.PurchaseLines))
                {
                    if (IsPurchaseValid(purchase))
                    {
                        UpdateMedicinesBuyed(purchase.PurchaseLines);
                        purchase.Amount = CalculateAmountOfBuy(purchase.PurchaseLines);
                        _purchaseManagement.InsertPurchase(purchase);
                    }
                }
                else
                {
                    throw new PurchaseException("No hay stock para los medicamentos elegidos.");
                }
            //}
            //else
            //{
            //    throw new PurchaseException("Los medicamentos no pertenecen a la misma farmacia.");
            //}
            return purchase.IdPurchase;
        }

        private string CreateId()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }

        private void SetIdsLinesOfPurchase(List<PurchaseLine> lines)
        {
            foreach (PurchaseLine line in lines)
            {
                line.IdPurchaseLine = CreateId();
            }
        }

        private bool MedicinesOfTheSamePharmacy(List<PurchaseLine> lines)
        {
            List<Medicine> medicinesToBuy = GetMedicinesOfPurchase(lines);
            List<Pharmacy> pharmaciesOfDataBase = _pharmacyManagement.GetPharmacies().ToList();
            int quantityOfMedicinesToBuy = medicinesToBuy.Count;
            if(quantityOfMedicinesToBuy > 0)
            {
                foreach (Pharmacy pharmacy in pharmaciesOfDataBase)
                {
                    int coincidences = 0;
                    foreach (Medicine medicineOfPharamacy in pharmacy.Medicines)
                    {
                        foreach (Medicine medicineToBuy in medicinesToBuy)
                        {
                            if (medicineToBuy.Code.Equals(medicineOfPharamacy.Code))
                            {
                                coincidences++;
                            }
                        }
                    }
                    bool areNotInSamePharmacy = 0 < coincidences && coincidences < quantityOfMedicinesToBuy;
                    if (areNotInSamePharmacy)
                    {
                        return false;
                    }
                }
            }
            else
            {
                throw new PurchaseException("Compra inválida, debe elegir al menos un medicamento a comprar.");
            }
            return true;
        }

        private bool ThereIsStock(List<PurchaseLine> lines)
        {
            List<Medicine> medicinesToBuy = GetMedicinesOfPurchase(lines);
            foreach (Medicine medicineToBuy in medicinesToBuy)
            {
                if(medicineToBuy.Stock < 1)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Medicine> GetMedicinesOfPurchase(List<PurchaseLine> lines)
        {
            List<Medicine> medicines = new List<Medicine>();
            foreach (PurchaseLine line in lines)
            {
                string code = line.MedicineCode;
                Medicine medicineOfLine = _medicineManagement.GetMedicineByCode(code);
                if(medicineOfLine == null)
                {
                    throw new NotFoundException("No existe el medicamento a comprar.");
                }
                medicines.Add(medicineOfLine);
            }
            return medicines;
        }

        private void UpdateMedicinesBuyed(List<PurchaseLine> lines)
        {
            List<Medicine> medicines = new List<Medicine>();
            foreach (PurchaseLine line in lines)
            {
                string code = line.MedicineCode;
                Medicine medicineFromDataBase = _medicineManagement.GetMedicineByCode(code);
                medicineFromDataBase.Stock -= line.MedicineQuantity;
                if(medicineFromDataBase.Stock < 0)
                {
                    medicineFromDataBase.Stock = 0;
                }
                _medicineManagement.UpdateMedicine(medicineFromDataBase);
            }
        }

        private double CalculateAmountOfBuy(List<PurchaseLine> lines)
        {
            double amount = 0;
            foreach (PurchaseLine line in lines)
            {
                Medicine medicineToBuy = _medicineManagement.GetMedicineByCode(line.MedicineCode);
                int quantity = line.MedicineQuantity;
                double price = medicineToBuy.Price;
                double totalCost = quantity* price;
                amount+=totalCost;
            }
            return amount;
        }


        private bool IsPurchaseValid(Purchase purchase)
        {
            if (purchase == null)
            {
                throw new PurchaseException("Compra inválida.");
            }
            if (purchase.IdPurchase == null || purchase.IdPurchase.Length < 1)
            {
                throw new PurchaseException("Identificador inválido.");
            }
            if (IsIdPurchaseRegistered(purchase.IdPurchase))
            {
                throw new PurchaseException("Ya existe una compra con el mismo identificador");
            }
            if (purchase.PurchaseLines == null || purchase.PurchaseLines.Count == 0)
            {
                throw new PurchaseException("Compra inválida.");
            }
            if (purchase.Amount < 0)
            {
                throw new PurchaseException("Monto inválido.");
            }
            if (purchase.BuyerEmail == null || purchase.BuyerEmail.Length < 1)
            {
                throw new PurchaseException("Email inválido.");
            }
            if (!IsEmailOK(purchase.BuyerEmail))
            {
                throw new PurchaseException("Email con formato inválido.");
            }
            return true;
        }

        public bool IsIdPurchaseRegistered(string idPurchase)
        {
            return _purchaseManagement.IsIdPurchaseRegistered(idPurchase);
        }

        // Font: https://stackoverflow.com/questions/5342375/regex-email-validation
        protected bool IsEmailOK(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
