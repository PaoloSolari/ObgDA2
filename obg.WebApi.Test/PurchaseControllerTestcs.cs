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


        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseService>(MockBehavior.Strict);
            api = new PurchaseController(mock.Object);
            validPurchaseLine = new PurchaseLine("bbbbb", "aaaaa", 2);
            validPurchase = new Purchase("111111", 100, "email@email.com");
            validPurchase.PurchaseLines.Add(validPurchaseLine);
            purchases = new List<Purchase>() { validPurchase };
        }

        [TestMethod]
        public void PostPurchaseBadRequest()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new PurchaseException());
            var result = api.PostPurchase(It.IsAny<Purchase>(), validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostPurchaseFail()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new Exception());
            var result = api.PostPurchase(It.IsAny<Purchase>(), validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostPurchaseOk()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Returns(validPurchase.IdPurchase);
            var result = api.PostPurchase(It.IsAny<Purchase>(), validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Compra " + validPurchase.IdPurchase + " exitosa.").Equals(body));
        }

        [TestMethod]
        public void PostPurchaseMedicineNotFound()
        {
            mock.Setup(x => x.InsertPurchase(It.IsAny<Purchase>())).Throws(new NotFoundException());
            var result = api.PostPurchase(It.IsAny<Purchase>(), validPurchase.BuyerEmail);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }
    }
}
