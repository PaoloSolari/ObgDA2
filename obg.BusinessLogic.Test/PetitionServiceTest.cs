using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class PetitionServiceTest
    {
        private Mock<IPetitionManagement> mock;
        private PetitionService service;
        private Petition validPetition1;
        private Petition validPetition2;
        private Petition nullPetition;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPetitionManagement>(MockBehavior.Strict);
            service = new PetitionService(mock.Object);
            validPetition1 = new Petition("aaaaa", 5);
            validPetition2 = new Petition("bbbbb", 10);
            nullPetition = null;
        }

        [TestMethod]
        public void InsertPetitionOK()
        {
            service.InsertPetition(validPetition1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_NullPetition()
        {
            service.InsertPetition(nullPetition);
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_NullMedicineCode()
        {
            validPetition1.MedicineCode = null;
            service.InsertPetition(validPetition1);
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_EmptyMedicineCode()
        {
            validPetition1.MedicineCode = "";
            service.InsertPetition(validPetition1);
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_NegativeNewQuantity()
        {
            validPetition1.NewQuantity = -5;
            service.InsertPetition(validPetition1);
        }
    }
}