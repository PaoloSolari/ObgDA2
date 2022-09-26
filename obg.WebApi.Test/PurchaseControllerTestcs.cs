using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
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


        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPurchaseService>(MockBehavior.Strict);
            api = new PurchaseController(mock.Object);
            validPurchaseLine = new PurchaseLine("aaaaa", 2);
            validPurchase = new Purchase(100, "email@email.com");
            validPurchase.PurchaseLines.Add(validPurchaseLine);
            purchases = new List<Purchase>() { validPurchase };
        }

        [TestMethod]
        public void GetMedicinesOk()
        {
            mock.Setup(x => x.GetPurchases()).Returns(purchases);

            var result = api.GetPurchases();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Purchase>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(purchases));
        }
    }
}
