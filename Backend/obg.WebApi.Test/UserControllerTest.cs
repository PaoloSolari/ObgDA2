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
    public class UserControllerTest
    {
        private Mock<IUserService> mock;
        private UserController api;
        private User validUser;
        private IEnumerable<User> users;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IUserService>(MockBehavior.Strict);
            api = new UserController(mock.Object);
            validUser = new User("Paolo", 123456, "ps@gmail.com", "password123.", "addressPS", RoleUser.Administrator, "12/09/2022");
            users = new List<User>() { validUser };
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void PostUserBadRequest()
        {
            mock.Setup(x => x.InsertUser(It.IsAny<User>())).Throws(new UserException());
            var result = api.PostUser(It.IsAny<User>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostUserFail()
        {
            mock.Setup(x => x.InsertUser(It.IsAny<User>())).Throws(new Exception());
            var result = api.PostUser(It.IsAny<User>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostUserOk()
        {
            mock.Setup(x => x.InsertUser(It.IsAny<User>())).Returns(validUser.Name);
            var result = api.PostUser(It.IsAny<User>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;
            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Usuario " + validUser.Name + " identificado. Ingrese email, contraseña y dirección.").Equals(body));
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void PutUserBadRequest()
        {
            mock.Setup(x => x.UpdateUser(validUser, validUser.Name)).Throws(new UserException());
            var result = api.PutUser(validUser, validUser.Name);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void PutUserNotFound()
        {
            mock.Setup(x => x.UpdateUser(validUser, validUser.Name)).Throws(new NotFoundException());
            var result = api.PutUser(validUser, validUser.Name);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PutUserFail()
        {
            mock.Setup(x => x.UpdateUser(validUser, validUser.Name)).Throws(new Exception());
            var result = api.PutUser(validUser, validUser.Name);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutUserOk()
        {
            var validUserModified = validUser;
            validUserModified.Password = "new password";
            mock.Setup(x => x.UpdateUser(validUserModified, validUser.Name)).Returns(validUserModified.Name);
            var result = api.PutUser(validUserModified, validUser.Name);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Registro del usuario " + validUser.Name + " exitoso.").Equals(body));
        }
    }
}
