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
        private Mock<IUserManagement> mockUser;
        private SessionService service;

        private Administrator validAdministrator1;
        private Administrator validAdministrator2;
        private Session validSession1;
        private Session validSession2;
        private Session nullSession;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISessionManagement>(MockBehavior.Strict);
            mockUser = new Mock<IUserManagement>(MockBehavior.Strict);
            service = new SessionService(mock.Object, mockUser.Object);

            validAdministrator1 = new Administrator("Juan Perez", 234567, "gssasaj@gmail.com", ".$dfdffaaa33", "18 de Julio", RoleUser.Administrator, "24/09/2021");
            validAdministrator2 = new Administrator("José Luis", 266567, "jssssa@gmail.com", ".$dfdffaaa33", "25 de Agosto", RoleUser.Administrator, "24/09/2021");

            validSession1 = new Session("DDIQDS", validAdministrator1.Name, "4de12a");
            validSession2 = new Session("XXYYZZ", validAdministrator2.Name, "FRR664");
            nullSession = null;
        }

        [TestMethod]
        public void InsertSessiondOK()
        {
            service.InsertSession(validSession1, validAdministrator1.Password);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullDemand()
        {
            service.InsertSession(nullSession, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullIdSession()
        {
            validSession1.IdSession = null;
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyIdSession()
        {
            validSession1.IdSession = "";
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_RepeatedIdSession()
        {
            service.InsertSession(validSession1, validAdministrator1.Password);
            validSession2.IdSession = "DDIQDS";
            service.InsertSession(validSession2, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullUserName()
        {
            validSession1.UserName = null;
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyUserName()
        {
            validSession1.UserName = "";
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_UserNameHasMore20Chars()
        {
            validSession1.UserName = "#aaabbbccc$aaabbbcccD";
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_InexistUserName()
        {
            validSession1.UserName = "..."; // Nos aseguramos de no introducirlo nunca.
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullToken()
        {
            validSession1.Token = null;
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyToken()
        {
            validSession1.Token = "";
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_LoggedToken()
        {
            service.InsertSession(validSession1, validAdministrator1.Password);
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

    }
}
