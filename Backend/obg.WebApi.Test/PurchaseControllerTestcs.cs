using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    public class PurchaseControllerTest
    {
        private Mock<IPurchaseService> mock;
        private PurchaseController api;
        private Purchase validPurchase;
        private PurchaseLine validPurchaseLine;
        private IEnumerable<Purchase> purchases;
        private List<PurchaseLine> purchaseLines;
        private Employee employee;
        private Session validSession;
        private Pharmacy validPharmacy;


        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseService>(MockBehavior.Strict);
            api = new PurchaseController(mock.Object);
            validPurchaseLine = new PurchaseLine("bbbbb", "aaaaa", 2);
            validPurchase = new Purchase("111111", 100, "email@email.com");
            validPharmacy = new Pharmacy("San Roque", "aaaa");
            validPurchase.PurchaseLines.Add(validPurchaseLine);
            employee = new Employee("Rodrigo", 123456, "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Employee, "13/09/2022", validPharmacy);
            validSession = new Session("DDIQDS", employee.Name, "4de12a");
            purchases = new List<Purchase>() { validPurchase };
        }

        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void PostPurchaseBadRequest()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new PurchaseException());
            var result = api.PostPurchase(validPurchase, validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostPurchaseFail()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new Exception());
            var result = api.PostPurchase(validPurchase, validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostPurchaseOk()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Returns(validPurchase.IdPurchase);
            var result = api.PostPurchase(validPurchase, validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Compra " + validPurchase.IdPurchase + " exitosa.").Equals(body));
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void PostPurchaseMedicineNotFound()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new NotFoundException());
            var result = api.PostPurchase(validPurchase, validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }
        [ExpectedException(typeof(PurchaseException))]
        [TestMethod]
        public void PutPurchaseBadRequest()
        {
            mock.Setup(x => x.UpdatePurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>())).Throws(new PurchaseException());
            var result = api.PutPurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void PutPurchaseNotFound()
        {
            mock.Setup(x => x.UpdatePurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>())).Throws(new NotFoundException());
            var result = api.PutPurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PutPurchaseFail()
        {
            mock.Setup(x => x.UpdatePurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PutPurchase(It.IsAny<string>(), It.IsAny<Purchase>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutDemandOk()
        {
            var validPurchaseModified = validPurchase;
            validPurchaseModified.IsConfirmed = true;
            mock.Setup(x => x.UpdatePurchase(validPurchaseModified.IdPurchase, validPurchaseModified, validSession.Token)).Returns(validPurchaseModified);
            var result = api.PutPurchase(validPurchaseModified.IdPurchase, validPurchaseModified, validSession.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validPurchaseModified.Equals(body));
        }
    }
}
