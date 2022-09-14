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
            pharmacy = new Pharmacy();
            validOwner1 = new Owner("Paolo", "aabbcc", "ps@gmail.com", "password123.", "addressPS", RoleUser.Owner, "12/09/2022", pharmacy);
            validOwner2 = new Owner("Gabriel", "xxyyzz", "gj@gmail.com", "password123.", "address", RoleUser.Owner, "12/09/2022", pharmacy);
            nullOwner = null;
        }
        [TestMethod]
        public void InsertOwnerOK()
        {
            service.InsertOwner(validOwner1);
            mock.VerifyAll();
        }

        // Administrator no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_OwnerNull()
        {
            service.InsertOwner(nullOwner);
            //mock.VerifyAll();
        }

        // Name no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InserlOwnerWrong_NullName()
        {
            validOwner1.Name = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Name no vacío.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyName()
        {
            validOwner1.Name = "";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Name único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedName()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Name = "Paolo";
            service.InsertOwner(validOwner2);
            //mock.VerifyAll();
        }

        // Name de máximo 20 caracteres.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NameHasMore20chars()
        {
            validOwner1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Code no null.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullCode()
        {
            validOwner1.Code = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Code no vacío.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyCode()
        {
            validOwner1.Code = "";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Code único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_TwoWithSameCodes()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Code = "aabbcc";
            service.InsertOwner(validOwner2);
            //mock.VerifyAll();
        }

        // Email no null
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWring_NullEmail()
        {
            validOwner1.Email = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Email no vacío
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_EmptyEmail()
        {
            validOwner1.Email = "";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Email único.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RepeatedEmail()
        {
            service.InsertOwner(validOwner1);
            validOwner2.Email = "ps@gmail.com";
            service.InsertOwner(validOwner2);
            //mock.VerifyAll();
        }

        // Email no format (@)
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NoFormatEmail()
        {
            validOwner1.Email = "psgmail.com";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        // Password mínimo 8 caracteres
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_PasswordHasLess8Chars()
        {
            validOwner1.Password = "aab#bcc";
            service.InsertOwner(validOwner1);
        }

        // Password al menos un caracter especial.
        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_PasswordHasNoSpecialChar()
        {
            validOwner1.Password = "aabbccdd";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_NullAddress()
        {
            validOwner1.Address = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        [TestMethod]
        public void GetOwnerRole_OK()
        {
            Assert.AreEqual(validOwner1.Role, RoleUser.Owner);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RegisterDateNull()
        {
            validOwner1.RegisterDate = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_RegisterDateEmpty()
        {
            validOwner1.RegisterDate = "";
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertOwnerWrong_HasNoPharmacy()
        {
            validOwner1.Pharmacy = null;
            service.InsertOwner(validOwner1);
            //mock.VerifyAll();
        }

        [TestMethod]
        public void GetOwnerPharmacy_OK()
        {
            Assert.AreEqual(validOwner1.Pharmacy, pharmacy);
            //mock.VerifyAll();
        }
    }
}
