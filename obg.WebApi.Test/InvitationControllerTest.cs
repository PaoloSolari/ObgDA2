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
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Mock<IInvitationService> mock;
        private InvitationController api;
        private Invitation validInvitation;
        private Pharmacy validPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IInvitationService>(MockBehavior.Strict);
            api = new InvitationController(mock.Object);
            validPharmacy = new Pharmacy("San Roque", "aaaa");
            validInvitation = new Invitation("CCCCCC", validPharmacy, RoleUser.Employee, "Paolo", 123456);
        }

        [TestMethod]
        public void PostInvitationBadRequest()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new InvitationException());
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostInvitationFail()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostInvitationOk()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Returns(validInvitation.UserCode);
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Código de invitación: " + validInvitation.UserCode).Equals(body));
        }
    }
}
