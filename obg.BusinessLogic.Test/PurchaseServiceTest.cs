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
    public class PurchaseServiceTest
    {
        private Mock<IPurchaseManagement> mock;
        private PurchaseService service;

        private List<PurchaseLine> purchaseLinesFromValidPurchase1;
        private List<PurchaseLine> purchaseLinesFromValidPurchase2;
        private List<PurchaseLine> emptyPurchaseLines;
        private PurchaseLine validPurchaseLine1;
        private PurchaseLine validPurchaseLine2;
        private Purchase validPurchase1;
        private Purchase validPurchase2;
        private Purchase nullPurchase;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseManagement>(MockBehavior.Strict);
            service = new PurchaseService(mock.Object);

            purchaseLinesFromValidPurchase1 = new List<PurchaseLine>();
            purchaseLinesFromValidPurchase2 = new List<PurchaseLine>();
            emptyPurchaseLines = new List<PurchaseLine>();
            validPurchaseLine1 = new PurchaseLine("JUKILA", "AQGTSR", 2);
            validPurchaseLine2 = new PurchaseLine("LJIHAY", "JAU7AS", 3);
            purchaseLinesFromValidPurchase1.Add(validPurchaseLine1);
            purchaseLinesFromValidPurchase2.Add(validPurchaseLine2);
            validPurchase1 = new Purchase("MANMAN", 100, "email@email.com");
            validPurchase2 = new Purchase("KILIJO", 200, "email@gmail.com");
            validPurchase1.PurchaseLines = purchaseLinesFromValidPurchase1;
            validPurchase2.PurchaseLines = purchaseLinesFromValidPurchase2;
            nullPurchase = null;
        }

        [TestCleanup]
        public void ResetDBs()
        {
            FakeDB.Purchases.Clear();
        }

        [TestMethod]
        public void InsertPurchaseOK()
        {
            service.InsertPurchase(validPurchase1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullPurchase()
        {
            service.InsertPurchase(nullPurchase);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullIdPurchase()
        {
            validPurchase1.IdPurchase = null;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_EmptyIdPurchase()
        {
            validPurchase1.IdPurchase = "";
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_RepeatedIdPurchase()
        {
            service.InsertPurchase(validPurchase1);
            validPurchase2.IdPurchase = "MANMAN";
            service.InsertPurchase(validPurchase2);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_NullPurchaseLines()
        {
            validPurchase1.PurchaseLines = null;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_EmptyPurchaseLines()
        {
            validPurchase1.PurchaseLines = emptyPurchaseLines;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullNegativeAmount()
        {
            validPurchase1.Amount = -2;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_NullEmail()
        {
            validPurchase1.BuyerEmail = null;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_EmptyEmail()
        {
            validPurchase1.BuyerEmail = "";
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseWrong_EmailHasNoFormat()
        {
            validPurchase1.BuyerEmail = "psgmail.com";
            service.InsertPurchase(validPurchase1);
        }

    }
}
