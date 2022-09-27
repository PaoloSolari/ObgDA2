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
using static System.Net.Mime.MediaTypeNames;

namespace obg.WebApi.Test
{
    [TestClass]
    public class DemandControllerTest
    {
        private Mock<IDemandService> mock;
        private DemandController api;
        private Demand validDemand;
        private Petition validPetition;
        private IEnumerable<Demand> demands;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IDemandService>(MockBehavior.Strict);
            api = new DemandController(mock.Object);
            validPetition = new Petition("aaaaa", 5);
            validDemand = new Demand(1, DemandStatus.InProgress);
            validDemand.Petitions.Add(validPetition);
            demands = new List<Demand>() { validDemand };
        }

        [TestMethod]
        public void GetDemandsOk()
        {
            mock.Setup(x => x.GetDemands()).Returns(demands);

            var result = api.GetDemands();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Demand>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(demands));
        }

        [TestMethod]
        public void GetDemandsFail()
        {
            mock.Setup(x => x.GetDemands()).Throws(new Exception());

            var result = api.GetDemands();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostDemandBadRequest()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>())).Throws(new DemandException());
            var result = api.PostDemand(It.IsAny<Demand>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostDemandFail()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>())).Throws(new Exception());
            var result = api.PostDemand(It.IsAny<Demand>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostDemandOk()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>())).Returns(validDemand);
            var result = api.PostDemand(It.IsAny<Demand>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Demand;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validDemand.Equals(body));
        }

        [TestMethod]
        public void PutDemandBadRequest()
        {
            mock.Setup(x => x.UpdateDemand(validDemand)).Throws(new DemandException());
            var result = api.PutDemand(validDemand.IdDemand, validDemand);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PutDemandNotFound()
        {
            mock.Setup(x => x.UpdateDemand(validDemand)).Throws(new NotFoundException());
            var result = api.PutDemand(validDemand.IdDemand, validDemand);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void PutDemandFail()
        {
            mock.Setup(x => x.UpdateDemand(validDemand)).Throws(new Exception());
            var result = api.PutDemand(validDemand.IdDemand, validDemand);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutDemandOk()
        {
            var validDemandModified = validDemand;
            validDemandModified.Status = DemandStatus.Accepted;
            mock.Setup(x => x.UpdateDemand(validDemandModified)).Returns(validDemandModified);
            var result = api.PutDemand(validDemand.IdDemand, validDemandModified);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Demand;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validDemandModified.Status.Equals(body.Status));
        }
    }
}
