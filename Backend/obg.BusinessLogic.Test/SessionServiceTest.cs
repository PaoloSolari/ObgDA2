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
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(false);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
            mock.VerifyAll();
            mockUser.VerifyAll();
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullSession()
        {
            service.InsertSession(nullSession, validAdministrator1.Password);
        }

        

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_RepeatedIdSession()
        {
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(false);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
            mock.VerifyAll();
            mockUser.VerifyAll();

            validSession2.IdSession = "DDIQDS"; 
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(true);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession2)).Returns(true);
            mock.Setup(x => x.InsertSession(validSession2));
            service.InsertSession(validSession2, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_NullUserName()
        {
            validSession1.UserName = null; 
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(false);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_EmptyUserName()
        {
            validSession1.UserName = ""; 
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(false);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_UserNameHasMore20Chars()
        {
            validSession1.UserName = "#aaabbbccc$aaabbbcccD"; 
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(false);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
        }



        [ExpectedException(typeof(SessionException))]
        [TestMethod]
        public void InsertSessionWrong_LoggedToken()
        {
            mock.Setup(x => x.IsIdSessionRegistered(It.IsAny<string>())).Returns(false);
            mockUser.Setup(x => x.GetUserByName(validAdministrator1.Name)).Returns(validAdministrator1);
            mock.Setup(x => x.IsNameLogged(validSession1)).Returns(true);
            mock.Setup(x => x.InsertSession(validSession1));
            service.InsertSession(validSession1, validAdministrator1.Password);
        }

    }
}
