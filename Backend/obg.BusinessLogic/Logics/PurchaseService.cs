﻿using obg.BusinessLogic.Interface.Interfaces;
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
        private readonly ISessionManagement _sessionManagement;
        private readonly IEmployeeManagement _employeeManagement;

        public PurchaseService(IPurchaseManagement purchaseManagement, IMedicineManagement medicineManagement, IPharmacyManagement pharmacyManagement, ISessionManagement sessionManagement, IEmployeeManagement employeeManagement)
        {
            _purchaseManagement = purchaseManagement;
            _medicineManagement = medicineManagement;
            _pharmacyManagement = pharmacyManagement;
            _sessionManagement = sessionManagement;
            _employeeManagement = employeeManagement;
        }

        public Purchase UpdatePurchase(string idPurchase, Purchase purchaseToUpdate, string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            Employee employee = _employeeManagement.GetEmployeeByName(session.UserName);
            Pharmacy employeePharmacy = employee.Pharmacy;
            List<Purchase> pharmacyPurchases = employeePharmacy.Purchases;
            if(pharmacyPurchases.Count == 0 || pharmacyPurchases == null)
            {
                throw new NotFoundException("No existen compras para esta farmacia.");
            }
            Purchase purchaseFromDB = _purchaseManagement.GetPurchaseById(idPurchase);
            if(purchaseFromDB == null)
            {
                throw new NotFoundException("La compra no existe.");
            }
            int counter = 0;
            bool allLinesConfirmed = true;
            List<PurchaseLine> purchasesLinesBuyed = new List<PurchaseLine>();
            foreach(PurchaseLine purchaseLine in purchaseFromDB.PurchaseLines)
            {
                Medicine medicine = _medicineManagement.GetMedicineByCode(purchaseLine.MedicineCode);
                if (employeePharmacy.Medicines.Contains(medicine))
                {
                    purchasesLinesBuyed.Add(purchaseLine);
                    purchaseLine.Status = purchaseToUpdate.PurchaseLines.ElementAt(counter).Status;
                    if (purchaseLine.Status.Equals(PurchaseLineStatus.UnResolved))
                    {
                        allLinesConfirmed = false;
                    }
                }
                counter++;
            }
            if (allLinesConfirmed)
            {
                purchaseFromDB.IsConfirmed = true;
                UpdateMedicinesBuyed(purchasesLinesBuyed);
                purchaseFromDB.Amount = CalculateAmountOfBuy(purchasesLinesBuyed);
            } 
            else
            {
                throw new PurchaseException("No puedes confirmar la compra si existe al menos un medicamento pendiente.");
            }
            return purchaseFromDB;
        }

        public IEnumerable<Purchase> GetPurchases(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            string employeeName = session.UserName;
            Employee employee = _employeeManagement.GetEmployeeByName(employeeName);
            Pharmacy pharmacyFromEmployee = employee.Pharmacy;
            List<Purchase> purchases = _purchaseManagement.GetPurchases().ToList();
            if(purchases.Count == 0 || purchases == null)
            {
                throw new NotFoundException("No existen compras para esta farmacia.");
            } 
            List<Purchase> purchasesFromEmployeePharmacy = new List<Purchase>();
            bool purchaseIsFromPharmacy = false;
            foreach(Purchase purchase in purchases)
            {
                foreach(PurchaseLine purchaseLine in purchase.PurchaseLines)
                {
                    Medicine medicine = _medicineManagement.GetMedicineByCode(purchaseLine.MedicineCode);
                    if (pharmacyFromEmployee.Medicines.Contains(medicine))
                    {
                        purchaseIsFromPharmacy = true;
                    }
                }
                if (purchaseIsFromPharmacy)
                {
                    purchasesFromEmployeePharmacy.Add(purchase);
                }
                purchaseIsFromPharmacy = false;
            }
            if(purchasesFromEmployeePharmacy.Count == 0 || purchasesFromEmployeePharmacy == null)
            {
                throw new NotFoundException("No existen compras para esta farmacia.");
            }
            return purchasesFromEmployeePharmacy;
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
                    //UpdateMedicinesBuyed(purchase.PurchaseLines);
                    //purchase.Amount = CalculateAmountOfBuy(purchase.PurchaseLines);
                    purchase.Amount = 1;
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
