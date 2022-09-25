using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class PharmacyControllerTest
    {
        private Mock<IPharmacyService> mock;
        private PharmacyController api;
        private Pharmacy validPharmacy;
        private IEnumerable<Pharmacy> pharmacies;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPharmacyService>(MockBehavior.Strict);
            api = new PharmacyController(mock.Object);
            validPharmacy = new Pharmacy("San Roque", "San Roque", null);
            pharmacies = new List<Pharmacy>() { validPharmacy };
        }

        [TestMethod]
        public void GetPharmaciesOk()
        {
            mock.Setup(x => x.GetPharmacies()).Returns(pharmacies);

            var result = api.GetPharmacies();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Pharmacy>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(pharmacies));
        }
    }
}
