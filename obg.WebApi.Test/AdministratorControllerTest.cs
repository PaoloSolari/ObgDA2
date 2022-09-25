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
    public class AdministratorControllerTest
    {
        private Mock<IAdministratorService> mock;
        private AdministratorController api;
        private Administrator validAdministrator;
        private IEnumerable<Administrator> administrators;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IAdministratorService>(MockBehavior.Strict);
            api = new AdministratorController(mock.Object);
            validAdministrator = new Administrator("Paolo", "ps@gmail.com", "password123.", "addressPS", RoleUser.Administrator, "12/09/2022", null);
            administrators = new List<Administrator>() { validAdministrator };
        }

        [TestMethod]
        public void GetPharmaciesOk()
        {
            mock.Setup(x => x.GetAdministrators()).Returns(administrators);

            var result = api.GetAdministrators();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Administrator>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(administrators));
        }
    }
}
