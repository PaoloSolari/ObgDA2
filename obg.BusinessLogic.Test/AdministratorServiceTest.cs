using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using Moq;
using System.Collections;
using System.Collections.Generic;
using obg.BusinessLogic.Logics;
using obg.Exceptions;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorServiceTest
    {
        private Mock<IAdministratorManagement> mock;
        private Administrator validAdministrator1;
        private Administrator validAdministrator2;
        private Administrator nullAdministrator;
        private AdministratorService service;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IAdministratorManagement>(MockBehavior.Strict);
            service = new AdministratorService(mock.Object);
            validAdministrator1 = new Administrator("Paolo", "aabbcc", "ps@gmail.com", "password123.", "addressPS", RoleUser.Administrator, "12/09/2022", null);
            validAdministrator2 = new Administrator("Gabriel", "xxyyzz", "gj@gmail.com", "password123.", "address", RoleUser.Administrator, "12/09/2022", null);
            nullAdministrator = null;
        }

        [TestMethod]
        public void InsertAdministratorOK()
        {
            service.InsertAdministrator(validAdministrator1);
            mock.VerifyAll();
        }

        // Administrator no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorNull()
        {
            service.InsertAdministrator(nullAdministrator);
            //mock.VerifyAll();
        }

        // Name no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorNullName()
        {
            validAdministrator1.Name = null;
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Name no vacío.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorEmptyName()
        {
            validAdministrator1.Name = "";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Name único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_TwoWithSameNames()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Name = "Paolo";
            service.InsertAdministrator(validAdministrator2);
            //mock.VerifyAll();
        }

        // Name de máximo 20 caracteres.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongName_More20chars()
        {
            validAdministrator1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Code no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorNullCode()
        {
            validAdministrator1.Code = null;
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Code no vacío.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorEmptyCode()
        {
            validAdministrator1.Code = "";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Code único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_TwoWithSameCodes()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Code = "aabbcc";
            service.InsertAdministrator(validAdministrator2);
            //mock.VerifyAll();
        }

        // Email no null
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorNullEmail()
        {
            validAdministrator1.Email = null;
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Email no vacío
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorEmptyEmail()
        {
            validAdministrator1.Email = "";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Email único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_TwoWithSameEmails()
        {
            service.InsertAdministrator(validAdministrator1);
            validAdministrator2.Email = "ps@gmail.com";
            service.InsertAdministrator(validAdministrator2);
            //mock.VerifyAll();
        }

        // Email no format (@)
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongEmail()
        {
            validAdministrator1.Email = "psgmail.com";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        // Password mínimo 8 caracteres
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongPassword_Less8Chars()
        {
            validAdministrator1.Password = "aab#bcc";
            service.InsertAdministrator(validAdministrator1);
        }

        // Password al menos un caracter especial.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongPassword_NoSpecialChar()
        {
            validAdministrator1.Password = "aabbccdd";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongAddress_Null()
        {
            validAdministrator1.Address = null;
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorRole_OK()
        {
            Assert.AreEqual(validAdministrator1.Role, RoleUser.Administrator);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongRegisterDate_Null()
        {
            validAdministrator1.RegisterDate = null;
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrongRegisterDate_Empty()
        {
            validAdministrator1.RegisterDate = "";
            service.InsertAdministrator(validAdministrator1);
            //mock.VerifyAll();
        }

    } 
} 
