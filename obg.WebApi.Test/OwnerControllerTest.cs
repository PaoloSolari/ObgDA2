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
    public class OwnerControllerTest
    {
        private Mock<IOwnerService> mock;
        private OwnerController api;
        private Owner validOwner;
        private IEnumerable<Owner> owners;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IOwnerService>(MockBehavior.Strict);
            api = new OwnerController(mock.Object);
            validOwner = new Owner("Paolo", "ps@gmail.com", "password123.", "addressPS", RoleUser.Owner, "12/09/2022", null);
            owners = new List<Owner>() { validOwner };
        }

        [TestMethod]
        public void GetPharmaciesOk()
        {
            mock.Setup(x => x.GetOwners()).Returns(owners);

            var result = api.GetOwners();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Administrator>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(owners));
        }
    }
}
