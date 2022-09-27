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
            var body = objectResult.Value as IEnumerable<Owner>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(owners));
        }

        [TestMethod]
        public void GetOwnersFail()
        {
            mock.Setup(x => x.GetOwners()).Throws(new Exception());

            var result = api.GetOwners();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostOwnerBadRequest()
        {
            mock.Setup(x => x.InsertOwner(It.IsAny<Owner>())).Throws(new UserException());
            var result = api.PostOwner(It.IsAny<Owner>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostOwnerFail()
        {
            mock.Setup(x => x.InsertOwner(It.IsAny<Owner>())).Throws(new Exception());
            var result = api.PostOwner(It.IsAny<Owner>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostOwnerOk()
        {
            mock.Setup(x => x.InsertOwner(It.IsAny<Owner>())).Returns(validOwner);
            var result = api.PostOwner(It.IsAny<Owner>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Owner;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validOwner.Equals(body));
        }

        [TestMethod]
        public void PutOwnerBadRequest()
        {
            mock.Setup(x => x.UpdateOwner(validOwner)).Throws(new UserException());
            var result = api.PutOwner(validOwner.Name, validOwner);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PutOwnerNotFound()
        {
            mock.Setup(x => x.UpdateOwner(validOwner)).Throws(new NotFoundException());
            var result = api.PutOwner(validOwner.Name, validOwner);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void PutOwnerFail()
        {
            mock.Setup(x => x.UpdateOwner(validOwner)).Throws(new Exception());
            var result = api.PutOwner(validOwner.Name, validOwner);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutOwnerOk()
        {
            var validOwnerModified = validOwner;
            validOwnerModified.Password = "new password";
            mock.Setup(x => x.UpdateOwner(validOwnerModified)).Returns(validOwnerModified);
            var result = api.PutOwner(validOwner.Name, validOwnerModified);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Owner;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validOwnerModified.Password.Equals(body.Password));
        }
    }
}
