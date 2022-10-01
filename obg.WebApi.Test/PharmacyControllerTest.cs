using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class PharmacyControllerTest
    {
        private Mock<IPharmacyService> mock;
        private PharmacyController api;
        private Pharmacy validPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPharmacyService>(MockBehavior.Strict);
            api = new PharmacyController(mock.Object);
            validPharmacy = new Pharmacy("FarmaShop", "San Roque", null);
        }

        [TestMethod]
        public void PostPharmacyBadRequest()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>())).Throws(new PharmacyException());
            var result = api.PostPharmacy(It.IsAny<Pharmacy>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostPharmacyFail()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>())).Throws(new Exception());
            var result = api.PostPharmacy(It.IsAny<Pharmacy>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostPharmacyOk()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>())).Returns(validPharmacy.Name);
            
            var result = api.PostPharmacy(It.IsAny<Pharmacy>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Nombre de la farmacia ingresada: " + (validPharmacy.Name)).Equals(body));
        }
    }
}
