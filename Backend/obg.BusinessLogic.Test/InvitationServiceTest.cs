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
        private Mock<IPharmacyManagement> mockPharmacy;
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
            mockPharmacy = new Mock<IPharmacyManagement>(MockBehavior.Strict);
            service = new InvitationService(mock.Object, mockPharmacy.Object);

            validPharmacy1 = new Pharmacy("San Roque", "Ejido");
            nullPharmacy = null;
            validInvitation1 = new Invitation("GGHHJJ", validPharmacy1, RoleUser.Employee, "José", 998911);
            validInvitation2 = new Invitation("AAANNN", validPharmacy1, RoleUser.Owner, "Camilo", 223344);
            nullInvitation = null;
        }

        [TestMethod]
        public void InsertInvitationOK()
        {
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);
            
            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertInvitationWrong_NullInvitation()
        {
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            service.InsertInvitation(nullInvitation, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_RepeatedIdInvitation()
        {
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();

            validInvitation2.IdInvitation = "GGHHJJ";

            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.IsNameRegistered(validInvitation2.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation2));

            service.InsertInvitation(validInvitation2, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_NullUserName()
        {
            validInvitation1.UserName = null;
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_EmptyUserName()
        {
            validInvitation1.UserName = "";
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_RepeatedUserName()
        {
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();

            validInvitation2.UserName = "José";
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation2.UserName)).Returns(true);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation2));

            service.InsertInvitation(validInvitation2, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_UserNameHasMore20Chars()
        {
            validInvitation1.UserName = "#aaabbbccc$aaabbbcccD";
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }

        [ExpectedException(typeof(InvitationException))]
        [TestMethod]
        public void InsertInvitationWrong_RepeatedUserCode()
        {
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation1.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.InsertInvitation(validInvitation1));

            service.InsertInvitation(validInvitation1, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();

            validInvitation2.UserCode = validInvitation1.UserCode;
            mockPharmacy.Setup(x => x.GetPharmacyByName(validPharmacy1.Name)).Returns(validPharmacy1);

            mock.Setup(x => x.IsIdInvitationRegistered(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsNameRegistered(validInvitation2.UserName)).Returns(false);
            mock.Setup(x => x.IsCodeRegistered(It.IsAny<int>())).Returns(true);
            mock.Setup(x => x.InsertInvitation(validInvitation2));

            service.InsertInvitation(validInvitation2, validPharmacy1.Name);

            mockPharmacy.VerifyAll();
            mock.VerifyAll();
        }
    }
}