using obg.BusinessLogic.Interface;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PurchaseLineService : IPurchaseLineService
    {
        private readonly IPurchaseLineManagement _purchaseLineManagement;
        private readonly ISessionManagement _sessionManagement;
        private readonly IEmployeeManagement _employeeManagement;
        private readonly IPurchaseManagement _purchaseManagement;
        private readonly IMedicineManagement _medicineManagement;

        public PurchaseLineService(IPurchaseLineManagement purchaseLineManagement, ISessionManagement sessionManagement, IEmployeeManagement employeeManagement, IPurchaseManagement purchaseManagement, IMedicineManagement medicineManagement)
        {
            _purchaseLineManagement = purchaseLineManagement;
            _sessionManagement = sessionManagement;
            _employeeManagement = employeeManagement;
            _purchaseManagement = purchaseManagement;
            _medicineManagement = medicineManagement;
        }

        public IEnumerable<PurchaseLine> GetPurchasesLines(string token, string idPurchase)
        {
            Purchase purchase = _purchaseManagement.GetPurchaseById(idPurchase);
            List<PurchaseLine> purchasesLines = purchase.PurchaseLines;
            Employee employee = GetUserByToken(token);
            Pharmacy employeePharmacy = employee.Pharmacy;
            List<PurchaseLine> purchasesLinesFromPharmacy = new List<PurchaseLine>();
            foreach (PurchaseLine purchaseLine in purchasesLines)
            {
                Medicine medicineOfPurchaseLine = _medicineManagement.GetMedicineByCode(purchaseLine.MedicineCode);
                if (employeePharmacy.Medicines.Contains(medicineOfPurchaseLine))
                {
                    purchasesLinesFromPharmacy.Add(purchaseLine);
                }
            }
            return purchasesLinesFromPharmacy;
        }

        public Employee GetUserByToken(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            string employeeName = session.UserName;
            Employee employee = _employeeManagement.GetEmployeeByName(employeeName);
            return employee;
        }

        public string UpdatePurchaseLine(string idPurchaseLine, PurchaseLine purchaseLine)
        {
            PurchaseLine purchaseLineFromDB = _purchaseLineManagement.GetPurchaseLineById(idPurchaseLine);
            if (purchaseLine.Status.Equals(PurchaseLineStatus.Accepted))
            {
                UpdateMedicineStock(purchaseLine);
            }
            purchaseLineFromDB.Status = purchaseLine.Status;
            _purchaseLineManagement.UpdatePurchaseLine(purchaseLineFromDB);
            return purchaseLineFromDB.IdPurchaseLine;
        }

        public void UpdateMedicineStock(PurchaseLine purchaseLine)
        {
            string medicineCode = purchaseLine.MedicineCode;
            Medicine medicine = _medicineManagement.GetMedicineByCode(medicineCode);
            medicine.Stock -= purchaseLine.MedicineQuantity;
            if (medicine.Stock < 0)
            {
                throw new MedicineException("No hay stock suficiente del medicamento " + medicine.Name);
            }
        }

    }
}
