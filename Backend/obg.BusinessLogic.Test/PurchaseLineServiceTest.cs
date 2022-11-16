using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class PurchaseLineServiceTest
    {
        private Mock<IPurchaseLineManagement> mock;
        private Mock<IPurchaseManagement> mockPurchase;
        private Mock<IMedicineManagement> mockMedicine;
        private Mock<ISessionManagement> mockSession;
        private Mock<IEmployeeManagement> mockEmployee;
        private PurchaseLineService service;
       
        private Employee validEmployee;
        private Session validSession;
        private List<PurchaseLine> purchaseLinesFromValidPurchase1;
        private List<PurchaseLine> purchaseLinesFromValidPurchase2;
        private List<PurchaseLine> emptyPurchaseLines;
        private PurchaseLine validPurchaseLine1;
        private PurchaseLine validPurchaseLine2;
        private Purchase validPurchase1;
        private Purchase validPurchase2;
        private Purchase nullPurchase;
        private Pharmacy validPharmacy1;
        private List<Pharmacy> pharmacies;
        private Medicine validMedicine1;


        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseLineManagement>(MockBehavior.Strict);
            mockPurchase = new Mock<IPurchaseManagement>(MockBehavior.Strict);
            mockMedicine = new Mock<IMedicineManagement>(MockBehavior.Strict);
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            mockEmployee = new Mock<IEmployeeManagement>(MockBehavior.Strict);
            service = new PurchaseLineService(mock.Object, mockSession.Object, mockEmployee.Object, mockPurchase.Object, mockMedicine.Object);


            purchaseLinesFromValidPurchase1 = new List<PurchaseLine>();
            purchaseLinesFromValidPurchase2 = new List<PurchaseLine>();
            emptyPurchaseLines = new List<PurchaseLine>();
            validPurchaseLine1 = new PurchaseLine("JUKILA", "XXCCAA", 2);
            validPurchaseLine2 = new PurchaseLine("LJIHAY", "XXCCAA", 3);
            purchaseLinesFromValidPurchase1.Add(validPurchaseLine1);
            purchaseLinesFromValidPurchase2.Add(validPurchaseLine2);
            validPurchase1 = new Purchase("MANMAN", 100, "email@email.com");
            validPurchase2 = new Purchase("KILIJO", 200, "email@gmail.com");
            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validMedicine1 = new Medicine("XXCCAA", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            validMedicine1.Stock = 100;
            pharmacies = new List<Pharmacy> { validPharmacy1 };
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validSession = new Session("DDIQDS", validEmployee.Name, "4de12a");

            validPurchase1.PurchaseLines = purchaseLinesFromValidPurchase1;
            validPurchase2.PurchaseLines = purchaseLinesFromValidPurchase2;
            nullPurchase = null;
        }

        [TestMethod]
        public void GetPurchasesLinesOK()
        {
            mockPurchase.Setup(x => x.GetPurchaseById(validPurchase1.IdPurchase)).Returns(validPurchase1);
            mockSession.Setup(x => x.GetSessionByToken(validSession.Token)).Returns(validSession);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession.UserName)).Returns(validEmployee);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);

            service.GetPurchasesLines(validSession.Token, validPurchase1.IdPurchase);
            mockPurchase.VerifyAll();
            mockSession.VerifyAll();
            mockEmployee.VerifyAll();
            mockMedicine.VerifyAll();
        }


        [TestMethod]
        public void GetUserByTokenOK()
        {
            mockSession.Setup(x => x.GetSessionByToken(validSession.Token)).Returns(validSession);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession.UserName)).Returns(validEmployee);

            service.GetUserByToken(validSession.Token);
            mockSession.VerifyAll();
            mockEmployee.VerifyAll();
        }

        [TestMethod]
        public void UpdatePurchaseLineOK()
        {
            mock.Setup(x => x.GetPurchaseLineById(validPurchaseLine1.IdPurchaseLine)).Returns(validPurchaseLine1);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mock.Setup(x => x.UpdatePurchaseLine(validPurchaseLine1));

            validPurchaseLine1.Status = PurchaseLineStatus.Accepted;
            service.UpdatePurchaseLine(validPurchaseLine1.IdPurchaseLine, validPurchaseLine1);
            mock.VerifyAll();
            mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void UpdateMedicineStockOK()
        {
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);

            service.UpdateMedicineStock(validPurchaseLine1);
            mockMedicine.VerifyAll();
        }
    }
}
