using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Text;
using obg.BusinessLogic.Logics;
using obg.Exceptions;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class PharmacyServiceTest
    {
        private Mock<IPharmacyManagement> mock;
        private Mock<ISessionManagement> mockSession;
        private PharmacyService service;

        private Pharmacy validPharmacy1;
        private Pharmacy validPharmacy2;
        private Pharmacy nullPharmacy;

        private Administrator administrator;
        private Session session;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPharmacyManagement>(MockBehavior.Strict);
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            service = new PharmacyService(mock.Object, mockSession.Object);

            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validPharmacy2 = new Pharmacy("FarmaArg", "Tristan");

            administrator = new Administrator("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022");
            administrator.Pharmacies.Add(validPharmacy1);
            session = new Session("123456", "Lucas", "XXYYZZ");

            nullPharmacy = null;
        }

        [TestMethod]
        public void InsertPharmacyOK()
        {
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullPharmacy()
        {
            service.InsertPharmacy(nullPharmacy, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullName()
        {
            validPharmacy1.Name = null;
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_EmptyName()
        {
            validPharmacy1.Name = "";
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_RepeatedName()
        {
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
            mock.VerifyAll();
            mockSession.VerifyAll(); 

            validPharmacy2.Name = "FarmaUy";

            mock.Setup(x => x.IsNameRegistered(validPharmacy2.Name)).Returns(true);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy2, session));

            service.InsertPharmacy(validPharmacy2, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertOwnerWrong_NameHasMore50chars()
        {
            validPharmacy1.Name = "#aaabbbccc$aaabbbcccD#aaabbbccc$aaabbbcccD#aaabbbccc$aaabbbcccD";
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullAddress()
        {
            validPharmacy1.Address = null;
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_EmptyAddress()
        {
            validPharmacy1.Address = "";
            mock.Setup(x => x.IsNameRegistered(validPharmacy1.Name)).Returns(false);
            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);
            mock.Setup(x => x.InsertPharmacy(validPharmacy1, session));

            service.InsertPharmacy(validPharmacy1, session.Token);
        }

    }
}
