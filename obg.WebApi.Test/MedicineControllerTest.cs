using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
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
            validMedicine = new Medicine("Paracetamol", "aaaa", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
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
    }
}
