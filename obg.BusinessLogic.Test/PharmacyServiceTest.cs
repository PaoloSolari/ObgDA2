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
        private PharmacyService service;
        private Pharmacy validPharmacy1;
        private Pharmacy validPharmacy2;
        private Pharmacy nullPharmacy;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPharmacyManagement>(MockBehavior.Strict);
            service = new PharmacyService(mock.Object);
            validPharmacy1 = new Pharmacy("San Roque", "aaaa", null);
            validPharmacy2 = new Pharmacy("Farmacity", "aaaa", null);
            nullPharmacy = null;
        }

        [TestMethod]
        public void InsertPharmacyOK()
        {
            service.InsertPharmacy(validPharmacy1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullPharmacy()
        {
            service.InsertPharmacy(nullPharmacy);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullName()
        {
            validPharmacy1.Name = null;
            service.InsertPharmacy(validPharmacy1);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_EmptyName()
        {
            validPharmacy1.Name = "";
            service.InsertPharmacy(validPharmacy1);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_RepeatedName()
        {
            service.InsertPharmacy(validPharmacy1);
            validPharmacy2.Name = "San Roque";
            service.InsertPharmacy(validPharmacy2);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertOwnerWrong_NameHasMore50chars()
        {
            validPharmacy1.Name = "#aaabbbccc$aaabbbcccD#aaabbbccc$aaabbbcccD#aaabbbccc$aaabbbcccD";
            service.InsertPharmacy(validPharmacy1);
        }

        [ExpectedException(typeof(PharmacyException))]
        [TestMethod]
        public void InsertPharmacyWrong_NullAddress()
        {
            validPharmacy1.Address = null;
            service.InsertPharmacy(validPharmacy1);
        }


    }
}
