using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class SessionServiceTest
    {
        private Mock<ISessionManagement> mock;
        private SessionService service;

        private Owner validOwner;
        private Session validSession1;
        private Session validSession2;
        private Session nullSession;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISessionManagement>(MockBehavior.Strict);
            service = new SessionService(mock.Object);

            validOwner = new Owner("Juan Perez", 234567, "ghj@gmail.com", ".$dfdffaaa33", "18 de Julio", RoleUser.Administrator, "24/09/2021", null);
            FakeDB.Users.Add(validOwner);
            //FakeDB.Administrators.Add(validAdministrator);
            //validSession1 = new Session("AABBCC", "Francisco", "4de12a");
            validSession1 = new Session("AABBCC", validOwner.Name, "4de12a");
            validSession2 = new Session("XXYYZZ", "Esteban", "FRR664");
            nullSession = null;
        }

        [TestMethod]
        public void InsertSessiondOK()
        {
            service.InsertSession(validSession1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullDemand()
        {
            service.InsertSession(nullSession);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullIdSession()
        {
            validSession1.IdSession = null;
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyIdSession()
        {
            validSession1.IdSession = "";
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_RepeatedIdSession()
        {
            service.InsertSession(validSession1);
            validSession2.IdSession = "AABBCC";
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullUserName()
        {
            validSession1.UserName = null;
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyUserName()
        {
            validSession1.UserName = "";
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_UserNameHasMore20Chars()
        {
            validSession1.UserName = "#aaabbbccc$aaabbbcccD";
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_InexistUserName()
        {
            validSession1.UserName = "..."; // Nos aseguramos de no introducirlo nunca.
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_LoggedUserName()
        {
            service.InsertSession(validSession1);
            validSession2.UserName = "Francisco";
            service.InsertSession(validSession2);
        }


        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullToken()
        {
            validSession1.Token = null;
            service.InsertSession(validSession1);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyToken()
        {
            validSession1.Token = "";
            service.InsertSession(validSession1);
        }

    }
}
