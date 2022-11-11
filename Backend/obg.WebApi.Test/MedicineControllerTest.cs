using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.WebApi.Test
{
    [TestClass]
    public class MedicineControllerTest
    {
        private Mock<IMedicineService> mock;
        private MedicineController api;
        private Medicine validMedicine;
        private IEnumerable<Medicine> medicines;
        private Pharmacy validPharmacy1;
        private Session session;
        private Employee employee;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IMedicineService>(MockBehavior.Strict);
            api = new MedicineController(mock.Object);
            validPharmacy1 = new Pharmacy("San Roque", "Ejido");
            employee = new Employee("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022", validPharmacy1);
            session = new Session("123456", "Lucas", "XXYYZZ");
            validMedicine = new Medicine("123456", "Paracetamol", "aaaa", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            medicines = new List<Medicine>() { validMedicine };
        }

        [TestMethod]
        public void GetMedicinesOk()
        {
            mock.Setup(x => x.GetMedicines(employee.Name)).Returns(medicines);

            var result = api.GetMedicines(employee.Name);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Medicine>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(medicines));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetMedicinesFail()
        {
            mock.Setup(x => x.GetMedicines(It.IsAny<string>())).Throws(new Exception());

            var result = api.GetMedicines(It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [ExpectedException(typeof(MedicineException))]
        [TestMethod]
        public void PostMedicineBadRequest()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>(), It.IsAny<string>())).Throws(new MedicineException());
            var result = api.PostMedicine(It.IsAny<Medicine>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostMedicineFail()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>(), It.IsAny<string>())).Throws(new Exception());
            var result = api.PostMedicine(It.IsAny<Medicine>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostMedicineOk()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>(), It.IsAny<string>())).Returns(validMedicine.Code);
            var result = api.PostMedicine(It.IsAny<Medicine>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Código del medicamento: " + validMedicine.Code).Equals(body));
        }
    }
}
