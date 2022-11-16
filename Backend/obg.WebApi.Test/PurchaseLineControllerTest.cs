using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class PurchaseLineControllerTest
    {
        private Mock<IPurchaseLineService> mock;
        private PurchaseLineController api;
        private Purchase validPurchase;
        private PurchaseLine validPurchaseLine;
        private IEnumerable<Purchase> purchases;
        private List<PurchaseLine> purchasesLines;
        private Employee employee;
        private Session validSession;
        private Pharmacy validPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseLineService>(MockBehavior.Strict);
            api = new PurchaseLineController(mock.Object);
            validPurchaseLine = new PurchaseLine("bbbbb", "aaaaa", 2);
            purchasesLines = new List<PurchaseLine>();
            purchasesLines.Add(validPurchaseLine);
            validPurchase = new Purchase("111111", 100, "email@email.com");
            validPharmacy = new Pharmacy("San Roque", "aaaa");
            validPurchase.PurchaseLines = purchasesLines;
            employee = new Employee("Rodrigo", 123456, "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Employee, "13/09/2022", validPharmacy);
            validSession = new Session("DDIQDS", employee.Name, "4de12a");
            purchases = new List<Purchase>() { validPurchase };
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void PutPurchaseLineBadRequest()
        {
            mock.Setup(x => x.UpdatePurchaseLine(It.IsAny<string>(), It.IsAny<PurchaseLine>())).Throws(new MedicineException());
            var result = api.PutPurchaseLine(It.IsAny<string>(), It.IsAny<PurchaseLine>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PutPurchaseLineFail()
        {
            mock.Setup(x => x.UpdatePurchaseLine(It.IsAny<string>(), It.IsAny<PurchaseLine>())).Throws(new Exception());
            var result = api.PutPurchaseLine(It.IsAny<string>(), It.IsAny<PurchaseLine>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutPurchaseLineOk()
        {
            var validPurchaseLineModified = validPurchaseLine;
            validPurchaseLineModified.Status = PurchaseLineStatus.Accepted;
            mock.Setup(x => x.UpdatePurchaseLine(validPurchaseLine.IdPurchaseLine, validPurchaseLineModified)).Returns(validPurchaseLineModified.IdPurchaseLine);
            var result = api.PutPurchaseLine(validPurchaseLine.IdPurchaseLine, validPurchaseLineModified);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validPurchaseLineModified.IdPurchaseLine.Equals(body));
        }

        [TestMethod]
        public void GetPurchaseLinesOk()
        {
            mock.Setup(x => x.GetPurchasesLines("4dd12a", validPurchase.IdPurchase)).Returns(purchasesLines);

            var result = api.GetPurchasesLines("4dd12a", validPurchase.IdPurchase);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<PurchaseLine>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(purchasesLines));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetPurchaseLinesFail()
        {
            mock.Setup(x => x.GetPurchasesLines("4dd12a", validPurchase.IdPurchase)).Throws(new Exception());

            var result = api.GetPurchasesLines("4dd12a", validPurchase.IdPurchase);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }
    }
}
