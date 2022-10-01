using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
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

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISessionService>(MockBehavior.Strict);
            api = new SessionController(mock.Object);
            validSession = new Session("123456", "Paolo", "AABBCC");
        }

        [TestMethod]
        public void PostPharmacyBadRequest()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>())).Throws(new SessionException());
            var result = api.PostSession(It.IsAny<Session>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostSessionFail()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>())).Throws(new Exception());
            var result = api.PostSession(It.IsAny<Session>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostSessionOk()
        {
            mock.Setup(x => x.InsertSession(It.IsAny<Session>())).Returns(validSession.Token);
            var result = api.PostSession(It.IsAny<Session>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Token: " + (validSession.Token)).Equals(body));
        }
    }
}
