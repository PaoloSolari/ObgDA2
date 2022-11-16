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
    public class InvitationControllerTest
    {
        private Mock<IInvitationService> mock;
        private InvitationController api;
        private Invitation validInvitation;
        private Session validSession;
        private Administrator administrator;
        private List<Invitation> invitations;
        private Pharmacy validPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IInvitationService>(MockBehavior.Strict);
            api = new InvitationController(mock.Object);
            validPharmacy = new Pharmacy("San Roque", "aaaa");
            administrator = new Administrator("Rodrigo", 123456, "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Administrator, "13/09/2022");
            validSession = new Session("DDIQDS", administrator.Name, "4de12a");
            validInvitation = new Invitation("CCCCCC", validPharmacy, RoleUser.Employee, "Paolo", 123456, administrator.Name);

            invitations = new List<Invitation>();
            invitations.Add(validInvitation);
        }

        [TestMethod]
        public void GetInvitationsOk()
        {
            mock.Setup(x => x.GetInvitations(validSession.Token)).Returns(invitations);

            var result = api.GetInvitations(validSession.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Invitation>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(invitations));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetDemandsFail()
        {
            mock.Setup(x => x.GetInvitations(validSession.Token)).Throws(new Exception());

            var result = api.GetInvitations(validSession.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void PostInvitationBadRequest()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new InvitationException());
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostInvitationFail()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostInvitationOk()
        {
            mock.Setup(x => x.InsertInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>())).Returns(validInvitation.UserCode);
            var result = api.PostInvitation(It.IsAny<Invitation>(), It.IsAny<string>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue((validInvitation.UserCode).Equals(body));
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void PutInvitationBadRequest()
        {
            mock.Setup(x => x.UpdateInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new InvitationException());
            var result = api.PutInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void PutInvitationNotFound()
        {
            mock.Setup(x => x.UpdateInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new NotFoundException());
            var result = api.PutInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PutInvitationFail()
        {
            mock.Setup(x => x.UpdateInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PutInvitation(It.IsAny<Invitation>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutInvitationOk()
        {
            var validInvitationModified = validInvitation;
            validInvitationModified.UserRole = RoleUser.Owner;
            mock.Setup(x => x.UpdateInvitation(validInvitationModified, validSession.Token));
            var result = api.PutInvitation(validInvitationModified, validSession.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Modificación exitosa").Equals(body));
        }
    }
}
