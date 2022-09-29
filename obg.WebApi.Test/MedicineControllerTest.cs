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

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IMedicineService>(MockBehavior.Strict);
            api = new MedicineController(mock.Object);
            validMedicine = new Medicine("123456", "Paracetamol", "aaaa", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            medicines = new List<Medicine>() { validMedicine };
        }

        [TestMethod]
        public void GetMedicinesOk()
        {
            mock.Setup(x => x.GetMedicines()).Returns(medicines);

            var result = api.GetMedicines();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Medicine>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(medicines));
        }

        [TestMethod]
        public void GetMedicinesFail()
        {
            mock.Setup(x => x.GetMedicines()).Throws(new Exception());

            var result = api.GetMedicines();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostMedicineBadRequest()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>())).Throws(new MedicineException());
            var result = api.PostMedicine(It.IsAny<Medicine>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostMedicineFail()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>())).Throws(new Exception());
            var result = api.PostMedicine(It.IsAny<Medicine>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostMedicineOk()
        {
            mock.Setup(x => x.InsertMedicine(It.IsAny<Medicine>())).Returns(validMedicine);
            var result = api.PostMedicine(It.IsAny<Medicine>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Medicine;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validMedicine.Equals(body));
        }
    }
}
