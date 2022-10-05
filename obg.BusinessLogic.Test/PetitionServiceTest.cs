using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using obg.Domain.Enums;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class PetitionServiceTest
    {
        private Mock<IPetitionManagement> mock;
        private PetitionService service;

        private Medicine validMedicine;
        private Petition validPetition1;
        private Petition validPetition2;
        private Petition nullPetition;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPetitionManagement>(MockBehavior.Strict);
            service = new PetitionService(mock.Object);

            validMedicine = new Medicine("AAMMOO", "Remedio", "Dolores", PresentationMedicine.Capsulas, 10, "25g", 300, false, true);
            validPetition1 = new Petition("UUUWWW", validMedicine.Code, 5);
            validPetition2 = new Petition("ASDASD", validMedicine.Code, 10);
            nullPetition = null;
        }

        [TestMethod]
        public void InsertPetitionOK()
        {
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));

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
        public void InsertPetitionWrong_NullIdPetition()
        {
            validPetition1.IdPetition = null;
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_EmptyIdPetition()
        {
            validPetition1.IdPetition = "";
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();

        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_RepeatedIdPetition()
        {
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();

            validPetition2.IdPetition = "UUUWWW";

            mock.Setup(x => x.IsIdPetitionRegistered(validPetition2.IdPetition)).Returns(true);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition2.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition2));
            service.InsertPetition(validPetition2);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_NullMedicineCode()
        {
            validPetition1.MedicineCode = null;
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();

        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_EmptyMedicineCode()
        {
            validPetition1.MedicineCode = "";
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();

        }

        [ExpectedException(typeof(PetitionException))]
        [TestMethod]
        public void InsertPetitionWrong_NegativeNewQuantity()
        {
            validPetition1.NewQuantity = -5;
            mock.Setup(x => x.IsIdPetitionRegistered(validPetition1.IdPetition)).Returns(false);
            mock.Setup(x => x.IsMedicineCodeOk(validPetition1.MedicineCode)).Returns(true);
            mock.Setup(x => x.InsertPetition(validPetition1));
            service.InsertPetition(validPetition1);
            mock.VerifyAll();

        }
    }
}