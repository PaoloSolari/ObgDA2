using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
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
        private Purchase validPurchase1;
        private Purchase validPurchase2;
        private PurchaseLine validPurchaseLine1;
        private PurchaseLine validPurchaseLine2;
        private List<PurchaseLine> emptyPurchaseLines;
        private Purchase nullPurchase;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseManagement>(MockBehavior.Strict);
            service = new PurchaseService(mock.Object);
            validPurchaseLine1 = new PurchaseLine("aaaaa", 2);
            validPurchaseLine2 = new PurchaseLine("bbbbb", 3);
            validPurchase1 = new Purchase(100, "email@email.com");
            validPurchase1.PurchaseLines.Add(validPurchaseLine1);
            validPurchase2 = new Purchase(200, "email@gmail.com");
            validPurchase2.PurchaseLines.Add(validPurchaseLine2);
            nullPurchase = null;
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
        public void InsertPurchaseLineWrong_NullEmail()
        {
            validPurchase1.BuyerEmail = null;
            service.InsertPurchase(validPurchase1);
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_EmptyEmail()
        {
            validPurchase1.BuyerEmail = "";
            service.InsertPurchase(validPurchase1);
        }
    }
}
