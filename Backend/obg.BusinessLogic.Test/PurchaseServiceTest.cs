﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class PurchaseServiceTest
    {
        private Mock<IPurchaseManagement> mock;
        private Mock<IPharmacyManagement> mockPharmacy;
        private Mock<IMedicineManagement> mockMedicine;
        private Mock<ISessionManagement> mockSession;
        private Mock<IEmployeeManagement> mockEmployee;
        private PurchaseService service;

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
        private List<Purchase> purchases;


        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseManagement>(MockBehavior.Strict);
            mockPharmacy = new Mock<IPharmacyManagement>(MockBehavior.Strict);
            mockMedicine = new Mock<IMedicineManagement>(MockBehavior.Strict);
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            mockEmployee = new Mock<IEmployeeManagement>(MockBehavior.Strict);
            service = new PurchaseService(mock.Object, mockMedicine.Object, mockPharmacy.Object, mockSession.Object, mockEmployee.Object);

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
            validPharmacy1.Medicines.Add(validMedicine1);
            pharmacies = new List<Pharmacy> { validPharmacy1 };
            validPharmacy1.Purchases.Add(validPurchase1);
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validSession = new Session("DDIQDS", validEmployee.Name, "4de12a");
            validPurchase1.PurchaseLines = purchaseLinesFromValidPurchase1;
            validPurchase2.PurchaseLines = purchaseLinesFromValidPurchase2;
            nullPurchase = null;
            purchases = new List<Purchase> { validPurchase1 };
        }

        [TestMethod]
        public void InsertPurchaseOK()
        {
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
            mock.VerifyAll();
            mockPharmacy.VerifyAll();
            mockMedicine.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullPurchase()
        {
            service.InsertPurchase(nullPurchase);
        }


        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_RepeatedIdPurchase()
        {
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
            mock.VerifyAll();
            mockPharmacy.VerifyAll();
            mockMedicine.VerifyAll();

            validPurchase2.IdPurchase = "MANMAN";
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(true);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase2));

            service.InsertPurchase(validPurchase2);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_NullPurchaseLines()
        {
            validPurchase1.PurchaseLines = null;
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_EmptyPurchaseLines()
        {
            validPurchase1.PurchaseLines = emptyPurchaseLines;
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullNegativeAmount()
        {
            validPurchase1.Amount = -2;
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullEmail()
        {
            validPurchase1.BuyerEmail = null;
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_EmptyEmail()
        {
            validPurchase1.BuyerEmail = "";
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_EmailHasNoFormat()
        {
            validPurchase1.BuyerEmail = "psgmail.com";
            mock.Setup(x => x.IsIdPurchaseRegistered(It.IsAny<string>())).Returns(false);
            mockPharmacy.Setup(x => x.GetPharmacies()).Returns(pharmacies);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mockMedicine.Setup(x => x.UpdateMedicine(validMedicine1));
            mock.Setup(x => x.InsertPurchase(validPurchase1));

            service.InsertPurchase(validPurchase1);
        }

        [TestMethod]
        public void GetPurchasesOk()
        {
            mockSession.Setup(x => x.GetSessionByToken(validSession.Token)).Returns(validSession);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession.UserName)).Returns(validEmployee);
            mock.Setup(x => x.GetPurchases()).Returns(purchases);
            mockMedicine.Setup(x => x.GetMedicineByCode(validMedicine1.Code)).Returns(validMedicine1);
            mock.Setup(x => x.UpdatePurchase(validPurchase1));

            service.GetPurchases(validSession.Token);
        }

    }
}
