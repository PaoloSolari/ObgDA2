﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class OwnerServiceTest
    {
        private Mock<IOwnerManagement> mock;
        private Mock<IPharmacyManagement> mockPharmacy;
        private Mock<IInvitationManagement> mockInvitationManagement;
        private OwnerService service;

        private Pharmacy pharmacy1;
        private Pharmacy pharmacy2;
        private Owner validOwner1;
        private Owner validOwner2;
        private Owner nullOwner;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IOwnerManagement>(MockBehavior.Strict);
            mockPharmacy = new Mock<IPharmacyManagement>(MockBehavior.Strict);
            mockInvitationManagement = new Mock<IInvitationManagement>(MockBehavior.Strict);
            service = new OwnerService(mock.Object, mockPharmacy.Object, mockInvitationManagement.Object);

            pharmacy1 = new Pharmacy("Farmashop", "18 de Julio");
            pharmacy2 = new Pharmacy("FarmaciaLibre", "25 de Agosto");
            validOwner1 = new Owner("Pedro", 000011, "p@gmail.com", "password123.", "addressP", RoleUser.Owner, "12/09/2022", pharmacy1);
            validOwner2 = new Owner("Juan", 000012, "j@gmail.com", "password123.", "addressJ", RoleUser.Owner, "12/09/2022", pharmacy2);
            nullOwner = null;
        }

        [TestMethod]
        public void InsertOwnerOK()
        {
            service.InsertOwner(validOwner1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullOwner()
        {
            service.InsertOwner(nullOwner);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullName()
        {
            validOwner1.Name = null;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyName()
        {
            validOwner1.Name = "";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedName()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Name = "Pedro";
            service.InsertOwner(validOwner2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NameHasMore20chars()
        {
            validOwner1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_CodeHasLess6Digits()
        {
            validOwner1.Code = 55555;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_CodeHasMore6Digits()
        {
            validOwner1.Code = 7777777;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedCode()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Code = validOwner1.Code;
            service.InsertOwner(validOwner2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWring_NullEmail()
        {
            validOwner1.Email = null;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyEmail()
        {
            validOwner1.Email = "";
            service.InsertOwner(validOwner1);
        }

        // Email único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedEmail()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Email = "p@gmail.com";
            service.InsertOwner(validOwner2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmailHasNoFormat()
        {
            validOwner1.Email = "psgmail.com";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_PasswordHasLess8Chars()
        {
            validOwner1.Password = "aab#bcc";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_PasswordHasNoSpecialChar()
        {
            validOwner1.Password = "aabbccdd";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullAddress()
        {
            validOwner1.Address = null;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertAdministratorWrong_RoleIncorrectEmployee()
        {
            validOwner1.Role = RoleUser.Employee;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RoleIncorrectAdmnistrator()
        {
            validOwner1.Role = RoleUser.Administrator;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullRegisterDate()
        {
            validOwner1.RegisterDate = null;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyRegisterDate()
        {
            validOwner1.RegisterDate = "";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullPharmacy()
        {
            validOwner1.Pharmacy = null;
            service.InsertOwner(validOwner1);
        }

    }
}
