using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using obg.ExporterInterface;
using obg.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace obg.WebApi.Test
{
    [TestClass]
    public class ExporterControllerTest
    {
        private Mock<IExporterService> mock;
        private ExporterController api;

        private Session validSession1;
        private List<string> dlls;
        private List<string> medicineCodes;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IExporterService>(MockBehavior.Strict);
            api = new ExporterController(mock.Object);
            dlls = new List<string>() { "JSON", "XML" };
            medicineCodes = new List<string>() { "123456", "abcdef" };
        }

        [TestMethod]
        public void GetExportersOk()
        {
            mock.Setup(x => x.GetExporters()).Returns(dlls);

            var result = api.GetExporters();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<string>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(dlls));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetDemandsFail()
        {
            mock.Setup(x => x.GetExporters()).Throws(new Exception());

            var result = api.GetExporters();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [ExpectedException(typeof(ExportException))]
        [TestMethod]
        public void ExportMedicineBadRequest()
        {
            mock.Setup(x => x.ExportMedicine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string,string>>())).Throws(new ExportException());
            var result = api.ExportMedicine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostInvitationFail()
        {
            mock.Setup(x => x.ExportMedicine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Throws(new Exception());
            var result = api.ExportMedicine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void ExportMedicineOk()
        {
            mock.Setup(x => x.ExportMedicine("JSON", It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns("JSON");
            var result = api.ExportMedicine("JSON", It.IsAny<string>(), It.IsAny<Dictionary<string, string>>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue("JSON".Equals(body));
        }
    }
}