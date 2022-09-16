using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private Owner validOwner1;
        private Owner validOwner2;
        private Owner nullOwner;
        private OwnerService service;
        private Pharmacy pharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IOwnerManagement>(MockBehavior.Strict);
            service = new OwnerService(mock.Object);

            validOwner1 = new Owner("Paolo", "aabbcc", "ps@gmail.com", "password123.", "addressPS", RoleUser.Owner, "12/09/2022", null);
            validOwner2 = new Owner("Gabriel", "xxyyzz", "gj@gmail.com", "password123.", "address", RoleUser.Owner, "12/09/2022", null);
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
        public void InserlOwnerWrong_NullName()
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
            validOwner2.Name = "Paolo";
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
        public void InsertOwnerWrong_NullCode()
        {
            validOwner1.Code = null;
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyCode()
        {
            validOwner1.Code = "";
            service.InsertOwner(validOwner1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedCode()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Code = "aabbcc";
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
            validOwner2.Email = "ps@gmail.com";
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
