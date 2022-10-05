﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class MedicineServiceTest
    {
        private Mock<IMedicineManagement> mock;
        private Mock<ISessionManagement> mockSession;
        private MedicineService service;

        private Pharmacy validPharmacy1;

        private Medicine validMedicine1;
        private Medicine validMedicine2;
        private Medicine nullMedicine;

        private List<Medicine> medicines;

        private Employee employee;
        private Session session;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IMedicineManagement>(MockBehavior.Strict);
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            service = new MedicineService(mock.Object, mockSession.Object);

            validPharmacy1 = new Pharmacy("San Roque", "Ejido");

            validMedicine1 = new Medicine("XXCCAA", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            validMedicine2 = new Medicine("XF45GG", "Ibuprofeno", "Dolor de estómago", PresentationMedicine.Comprimidos, 0, "0.5mg", 100, false, true);
            nullMedicine = null;

            medicines = new List <Medicine> ();

            employee = new Employee("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022", validPharmacy1);
            session = new Session("123456", "Lucas", "XXYYZZ");
        }

        [TestMethod]
        public void InsertMedicineOK()
        {
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();

        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullMedicine()
        {
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(nullMedicine, session));

            service.InsertMedicine(nullMedicine, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullCode()
        {
            validMedicine1.Code = null;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(nullMedicine, session));

            service.InsertMedicine(nullMedicine, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyCode()
        {
            validMedicine1.Code = "";
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(nullMedicine, session));

            service.InsertMedicine(nullMedicine, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_RepeatedCode()
        {
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();

            validMedicine2.Code = "XXCCAA";

            medicines.Add(validMedicine1);
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine2, session));

            service.InsertMedicine(validMedicine2, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullName()
        {
            validMedicine1.Name = null;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyName()
        {
            validMedicine1.Name = "";
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullSymtompsItTreats()
        {
            validMedicine1.SymtompsItTreats = null;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptySymtompsItTreats()
        {
            validMedicine1.SymtompsItTreats = "";
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NegativeQuantity()
        {
            validMedicine1.Quantity = -1;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NullUnit()
        {
            validMedicine1.Unit = null;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_EmptyUnit()
        {
            validMedicine1.Unit = "";
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void InsertMedicineWrong_NegativePrice()
        {
            validMedicine1.Price = -1;
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            mockSession.Setup(x => x.GetSessionByToken(session.Token)).Returns(session);

            mock.Setup(x => x.InsertMedicine(validMedicine1, session));

            service.InsertMedicine(validMedicine1, session.Token);

            mock.VerifyAll();
            mockSession.VerifyAll();
        }
    }
}
