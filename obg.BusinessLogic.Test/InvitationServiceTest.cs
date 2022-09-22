using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class InvitationServiceTest
    {
        private Mock<IInvitationManagement> mock;
        private InvitationService service;
        private Invitation validInvitation1;
        private Invitation validInvitation2;
        private Pharmacy validPharmacy1;
        private Invitation nullInvitation;
        private Pharmacy nullPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IInvitationManagement>(MockBehavior.Strict);
            service = new InvitationService(mock.Object);
            nullPharmacy = null;
            validPharmacy1 = new Pharmacy("San Roque", "aaaa", null);
            validInvitation1 = new Invitation(1, validPharmacy1, RoleUser.Employee, "Paolo", "123456");
            validInvitation2 = new Invitation(2, validPharmacy1, RoleUser.Owner, "Gabriel", "135791");
            nullInvitation = null;
        }

        [TestMethod]
        public void InsertInvitationOK()
        {
            service.InsertInvitation(validInvitation1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_NullInvitation()
        {
            service.InsertInvitation(nullInvitation);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_NullPharmacy()
        {
            validInvitation1.Pharmacy = nullPharmacy;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_NullUserName()
        {
            validInvitation1.UserName = null;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_EmptyUserName()
        {
            validInvitation1.UserName = "";
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_NullUserCode()
        {
            validInvitation1.UserCode = null;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_EmptyUserCode()
        {
            validInvitation1.UserCode = "";
            service.InsertInvitation(validInvitation1);
        }
    }
}