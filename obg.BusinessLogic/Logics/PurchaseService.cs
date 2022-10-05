using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseManagement _purchaseManagement;

        public PurchaseService(IPurchaseManagement purchaseManagement)
        {
            _purchaseManagement = purchaseManagement;
        }

        public string InsertPurchase(Purchase purchase)
        {
            if (IsPurchaseValid(purchase))// && !IsCodeRegistered(medicine.Name))
            {
                _purchaseManagement.InsertPurchase(purchase);
            }
            return purchase.IdPurchase;
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
