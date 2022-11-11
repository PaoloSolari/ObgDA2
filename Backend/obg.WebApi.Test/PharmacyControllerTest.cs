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
        private Administrator administrator;
        private Session session;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPharmacyService>(MockBehavior.Strict);
            api = new PharmacyController(mock.Object);
            validPharmacy = new Pharmacy("FarmaShop", "San Roque");
            administrator = new Administrator("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022");
            //administrator.Pharmacies.Add(pharmacy);
            session = new Session("123456", "Lucas", "XXYYZZ");
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void PostPharmacyBadRequest()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>(),It.IsAny<string>())).Throws(new PharmacyException());
            var result = api.PostPharmacy(It.IsAny<Pharmacy>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostPharmacyFail()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PostPharmacy(It.IsAny<Pharmacy>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostPharmacyOk()
        {
            mock.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>(), It.IsAny<string>())).Returns(validPharmacy.Name);
            
            var result = api.PostPharmacy(It.IsAny<Pharmacy>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Nombre de la farmacia ingresada: " + (validPharmacy.Name)).Equals(body));
        }
    }
}
