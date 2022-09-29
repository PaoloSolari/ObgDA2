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

            validPharmacy1 = new Pharmacy("San Roque", "Ejido", null);
            nullPharmacy = null;
            validInvitation1 = new Invitation("GGHHJJ", validPharmacy1, RoleUser.Employee, "José", 998911);
            validInvitation2 = new Invitation("AAANNN", validPharmacy1, RoleUser.Owner, "Camilo", 223344);
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
        public void InsertInvitationWrong_NullIdInvitation()
        {
            validInvitation1.IdInvitation = null;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_EmptyIdInvitation()
        {
            validInvitation1.IdInvitation = "";
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_RepeatedIdInvitation()
        {
            service.InsertInvitation(validInvitation1);
            validInvitation2.IdInvitation = "GGHHJJ";
            service.InsertInvitation(validInvitation2);
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
        public void InsertInvitationWrong_RepeatedUserName()
        {
            service.InsertInvitation(validInvitation1);
            validInvitation2.UserName = "José";
            service.InsertInvitation(validInvitation2);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_UserNameHasMore20Chars()
        {
            validInvitation1.UserName = "#aaabbbccc$aaabbbcccD";
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_UserCodeHasLess6Digits()
        {
            validInvitation1.UserCode = 55555;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_UserCodeHasMore6Digits()
        {
            validInvitation1.UserCode = 7777777;
            service.InsertInvitation(validInvitation1);
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_RepeatedUserCode()
        {
            service.InsertInvitation(validInvitation1);
            validInvitation2.UserCode = validInvitation1.UserCode;
            service.InsertInvitation(validInvitation2);
        }
    }
}