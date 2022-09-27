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

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorServiceTest
    {
        private Mock<IAdministratorManagement> mock;
        private AdministratorService service;
        
        private Administrator validAdministrator1;
        private Administrator validAdministrator2;
        private Administrator nullAdministrator;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IAdministratorManagement>(MockBehavior.Strict);

            service = new AdministratorService(mock.Object);

            validAdministrator1 = new Administrator("Paolo", 000001, "ps@gmail.com", "password123.", "addressPS", RoleUser.Administrator, "12/09/2022", null);
            validAdministrator2 = new Administrator("Gabriel", 000002, "gj@gmail.com", "password123.", "addressGJ", RoleUser.Administrator, "12/09/2022", null);
            nullAdministrator = null;
        }

        [TestMethod]
        public void InsertAdministratorOK()
        {
            service.InsertAdministrator(validAdministrator1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministrator_NullAdminstrator()
        {
            service.InsertAdministrator(nullAdministrator);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_NullName()
        {
            validAdministrator1.Name = null;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_EmptyName()
        {
            validAdministrator1.Name = "";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RepeatedName()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Name = "Paolo";
            service.InsertAdministrator(validAdministrator2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_NameHasMore20chars()
        {
            validAdministrator1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_CodeHasLess6Digits()
        {
            validAdministrator1.Code = 55555;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_CodeHasMore6Digits()
        {
            validAdministrator1.Code = 7777777;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RepeatedCode()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Code = validAdministrator1.Code;
            service.InsertAdministrator(validAdministrator2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_NullEmail()
        {
            validAdministrator1.Email = null;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_EmptyEmail()
        {
            validAdministrator1.Email = "";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RepeatedEmail()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Email = "ps@gmail.com";
            service.InsertAdministrator(validAdministrator2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_EmailHasNoFormat()
        {
            validAdministrator1.Email = "psgmail.com";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_PasswordHasLess8Chars()
        {
            validAdministrator1.Password = "aab#bcc";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_PasswordHasNoSpecialChar()
        {
            validAdministrator1.Password = "aabbccdd";
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_NullAddress()
        {
            validAdministrator1.Address = null;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RoleIncorrectEmployee()
        {
            validAdministrator1.Role = RoleUser.Employee;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RoleIncorrectOwner()
        {
            validAdministrator1.Role = RoleUser.Owner;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_NullRegisterDate()
        {
            validAdministrator1.RegisterDate = null;
            service.InsertAdministrator(validAdministrator1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_EmptyRegisterDate()
        {
            validAdministrator1.RegisterDate = "";
            service.InsertAdministrator(validAdministrator1);
        }

    } 
} 
