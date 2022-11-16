using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class SessionControllerTest
    {
        private Mock<ISessionService> mock;
        private Session validSession;
        private SessionController api;
        private Administrator administrator;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISessionService>(MockBehavior.Strict);
            api = new SessionController(mock.Object);
            validSession = new Session("123456", "Paolo", "AABBCC");
            administrator = new Administrator("Paolo", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022");

        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void PostSessionBadRequest()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>(), It.IsAny<string>())).Throws(new SessionException());
            var result = api.PostSession(It.IsAny<Session>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostSessionFail()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PostSession(It.IsAny<Session>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostSessionOk()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>(), It.IsAny<string>())).Returns(validSession.Token);
            var result = api.PostSession(It.IsAny<Session>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue((validSession.Token).Equals(body));
        }
    }
}
