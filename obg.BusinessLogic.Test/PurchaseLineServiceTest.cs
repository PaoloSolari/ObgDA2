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
    public class PurchaseLineServiceTest
    {
        private Mock<IPurchaseLineManagement> mock;
        private PurchaseLineService service;
        private PurchaseLine validPurchaseLine1;
        private PurchaseLine validPurchaseLine2;
        private PurchaseLine nullPurchaseLine;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseLineManagement>(MockBehavior.Strict);
            service = new PurchaseLineService(mock.Object);
            validPurchaseLine1 = new PurchaseLine("aaaaa", 2);
            validPurchaseLine2 = new PurchaseLine("bbbbb", 3);
            nullPurchaseLine = null;
        }

        [TestMethod]
        public void InsertPurchaseLineOK()
        {
            service.InsertPurchaseLine(validPurchaseLine1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_NullPurchaseLine()
        {
            service.InsertPurchaseLine(nullPurchaseLine);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_NullMedicineCode()
        {
            validPurchaseLine1.MedicineCode = null;
            service.InsertPurchaseLine(validPurchaseLine1);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_EmptyName()
        {
            validPurchaseLine1.MedicineCode = "";
            service.InsertPurchaseLine(validPurchaseLine1);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_HasMedicineQuantityLessThan1()
        {
            validPurchaseLine1.MedicineQuantity = 0;
            service.InsertPurchaseLine(validPurchaseLine1);
        }
    }
}