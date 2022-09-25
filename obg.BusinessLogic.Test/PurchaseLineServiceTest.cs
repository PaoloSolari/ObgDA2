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
        private PurchaseLineService service;

        private Medicine validMedicine;
        private PurchaseLine validPurchaseLine1;
        private PurchaseLine validPurchaseLine2;
        private PurchaseLine nullPurchaseLine;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseLineManagement>(MockBehavior.Strict);
            service = new PurchaseLineService(mock.Object);

            validMedicine = new Medicine("GDY3AA", "Remedio", "Dolores", PresentationMedicine.Capsulas, 10, "25g", 300, false, true);
            FakeDB.Medicines.Add(validMedicine);
            validPurchaseLine1 = new PurchaseLine("HUDTGY", validMedicine.Code, 2);
            validPurchaseLine2 = new PurchaseLine("LJUAHl", validMedicine.Code, 3);
            nullPurchaseLine = null;
        }

        [TestCleanup]
        public void ResetDBs()
        {
            FakeDB.PurchaseLines.Clear();
            FakeDB.Medicines.Clear();
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
        public void InsertPurchaseLineWrong_NullIdPurchaseLine()
        {
            validPurchaseLine1.IdPurchaseLine = null;
            service.InsertPurchaseLine(validPurchaseLine1);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_EmptyIdPurchaseLine()
        {
            validPurchaseLine1.IdPurchaseLine = "";
            service.InsertPurchaseLine(validPurchaseLine1);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_RepeatedIdPurchaseLine()
        {
            service.InsertPurchaseLine(validPurchaseLine1);
            validPurchaseLine2.IdPurchaseLine = "HUDTGY";
            service.InsertPurchaseLine(validPurchaseLine2);
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
        public void InsertPurchaseLineWrong_EmptyMedicineCode()
        {
            validPurchaseLine1.MedicineCode = "";
            service.InsertPurchaseLine(validPurchaseLine1);
        }

        [ExpectedException(typeof(PurchaseLineException))]
        [TestMethod]
        public void InsertPurchaseLineWrong_InexistMedicineCode()
        {
            validPurchaseLine1.MedicineCode = "..."; // Nos aseguramos de no introducirlo nunca.
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