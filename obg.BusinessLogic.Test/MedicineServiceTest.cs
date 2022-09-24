using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class MedicineServiceTest
    {
        private Mock<IMedicineManagement> mock;
        private MedicineService service;

        private Medicine validMedicine1;
        private Medicine validMedicine2;
        private Medicine nullMedicine;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IMedicineManagement>(MockBehavior.Strict);
            service = new MedicineService(mock.Object);

            validMedicine1 = new Medicine("XXCCAA", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            validMedicine2 = new Medicine("XF45GG", "Ibuprofeno", "Dolor de estómago", PresentationMedicine.Comprimidos, 0, "0.5mg", 100, false, true);
            nullMedicine = null;
        }

        [TestMethod]
        public void InsertMedicineOK()
        {
            service.InsertMedicine(validMedicine1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullMedicine()
        {
            service.InsertMedicine(nullMedicine);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullCode()
        {
            validMedicine1.Code = null;
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyCode()
        {
            validMedicine1.Code = "";
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_RepeatedCode()
        {
            service.InsertMedicine(validMedicine1);
            validMedicine2.Name = "XXCCAA";
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullName()
        {
            validMedicine1.Name = null;
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyName()
        {
            validMedicine1.Name = "";
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullSymtompsItTreats()
        {
            validMedicine1.SymtompsItTreats = null;
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptySymtompsItTreats()
        {
            validMedicine1.SymtompsItTreats = "";
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NegativeQuantity()
        {
            validMedicine1.Quantity = -1;
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullUnit()
        {
            validMedicine1.Unit = null;
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyUnit()
        {
            validMedicine1.Unit = "";
            service.InsertMedicine(validMedicine1);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NegativePrice()
        {
            validMedicine1.Price = -1;
            service.InsertMedicine(validMedicine1);
        }
    }
}
