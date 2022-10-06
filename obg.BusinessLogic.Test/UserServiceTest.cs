using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using Moq;
using System.Collections;
using System.Collections.Generic;
using obg.BusinessLogic.Logics;
using obg.Exceptions;
using obg.BusinessLogic.Interface.Interfaces;
using System;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserManagement> mock;
        private Mock<IAdministratorManagement> mockAdministrator;
        private Mock<IEmployeeManagement> mockEmployee;
        private Mock<IOwnerManagement> mockOwner;
        private Mock<IInvitationManagement> mockInvitation;
        private User validUser1; 
        private User validUser2;
        private User nullUser;
        private Administrator administrator;
        private UserService service;
        private Invitation validInvitation1;
        private Invitation validInvitation2;
        private Pharmacy validPharmacy1;
        private List<User> users;



        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IUserManagement>(MockBehavior.Strict);
            mockAdministrator = new Mock<IAdministratorManagement>(MockBehavior.Strict);
            mockEmployee = new Mock<IEmployeeManagement>(MockBehavior.Strict);
            mockOwner = new Mock<IOwnerManagement>(MockBehavior.Strict);
            mockInvitation = new Mock<IInvitationManagement>(MockBehavior.Strict);

            service = new UserService(mock.Object, mockAdministrator.Object, mockOwner.Object, mockEmployee.Object, mockInvitation.Object);
            validPharmacy1 = new Pharmacy("San Roque", "Ejido");

            validInvitation1 = new Invitation("GGHHJJ", validPharmacy1, RoleUser.Administrator, "Lucas", 789099);
            validInvitation2 = new Invitation("GGZZJJ", validPharmacy1, RoleUser.Administrator, "Rodrigo", 123456);
            validUser2 = new User("Rodrigo", 123456, "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Employee, "13/09/2022");
            validUser1 = new User("Lucas", 789099, "lr@gmail.com", "###bbb123.", "address", RoleUser.Administrator, "13/09/2022");
            nullUser = null;
            users = new List<User> { validUser2};
            //administrator = new Administrator("Lucas", 789099, "lr@gmail.com", "###bbb123.", "address", RoleUser.Administrator, "13/09/2022");
        }

        [TestMethod]
        public void InsertUserOK()
        {
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertUserWrong_UserNull()
        {
            validUser1 = null;
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertUserWrong_NameNull()
        {
            validUser1.Name = null;
            validInvitation1.UserName = validUser1.Name;
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertUserWrong_NameEmpty()
        {
            validUser1.Name = "";
            validInvitation1.UserName = validUser1.Name;
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameRepeated()
        {
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();

            validUser2.Name = "Lucas";

            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation2.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation2.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser2);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameHasMore20Chars()
        {
            validUser1.Name = "#aaabbbccc$aaabbbcccD";
            validInvitation1.UserName = validUser1.Name;
            mockInvitation.Setup(x => x.GetInvitationByCode(validInvitation1.UserCode)).Returns(validInvitation1);
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(nullUser);
            mock.Setup(x => x.GetUsers()).Returns(users);
            mockAdministrator.Setup(x => x.InsertAdministrator(It.IsAny<Administrator>()));

            service.InsertUser(validUser1);

            mockInvitation.VerifyAll();
            mock.VerifyAll();
            mockAdministrator.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailNoFormat()
        {
            validUser1.Email = "psgmail.com";
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(validUser1);
            mock.Setup(x => x.UpdateUser(validUser1));
            service.UpdateUser(validUser1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordHasLess8Chars()
        {
            validUser1.Password = "aab#bcc";
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(validUser1);
            mock.Setup(x => x.UpdateUser(validUser1));
            service.UpdateUser(validUser1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordNoSpecialChar()
        {
            validUser1.Password = "aabbccdd";
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(validUser1);
            mock.Setup(x => x.UpdateUser(validUser1));
            service.UpdateUser(validUser1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_AddressNull()
        {
            validUser1.Address = null;
            mock.Setup(x => x.GetUserByName(validInvitation1.UserName)).Returns(validUser1);
            mock.Setup(x => x.UpdateUser(validUser1));
            service.UpdateUser(validUser1);
            mock.VerifyAll();
        }

    }
}
