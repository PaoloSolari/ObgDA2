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
        public void GetAdministratorsOk()
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

        [TestMethod]
        public void GetAdministratorsFail()
        {
            mock.Setup(x => x.GetAdministrators()).Throws(new Exception());

            var result = api.GetAdministrators();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostAdministratorBadRequest()
        {
            mock.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>())).Throws(new UserException());
            var result = api.PostAdministrator(It.IsAny<Administrator>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostAdministratorFail()
        {
            mock.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>())).Throws(new Exception());
            var result = api.PostAdministrator(It.IsAny<Administrator>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostAdministratorOk()
        {
            mock.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>())).Returns(validAdministrator);
            var result = api.PostAdministrator(It.IsAny<Administrator>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Administrator;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validAdministrator.Equals(body));
        }

        [TestMethod]
        public void PutAdministratorBadRequest()
        {
            mock.Setup(x => x.UpdateAdministrator(validAdministrator)).Throws(new UserException());
            var result = api.PutAdministrator(validAdministrator.Name, validAdministrator);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PutAdministratorNotFound()
        {
            mock.Setup(x => x.UpdateAdministrator(validAdministrator)).Throws(new NotFoundException());
            var result = api.PutAdministrator(validAdministrator.Name, validAdministrator);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void PutAdministratorFail()
        {
            mock.Setup(x => x.UpdateAdministrator(validAdministrator)).Throws(new Exception());
            var result = api.PutAdministrator(validAdministrator.Name, validAdministrator);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutAdministratorOk()
        {
            var validAdministratorModified = validAdministrator;
            validAdministratorModified.Password = "new password";
            mock.Setup(x => x.UpdateAdministrator(validAdministratorModified)).Returns(validAdministratorModified);
            var result = api.PutAdministrator(validAdministrator.Name, validAdministratorModified);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Administrator;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validAdministratorModified.Password.Equals(body.Password));
        }
    }
}
