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
        private Pharmacy validPharmacy1;

        private Session validSession1;
        private Session validSession2;
        private Employee validEmployee;
        private Owner validOwner;


        private IEnumerable<Demand> demands;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IDemandService>(MockBehavior.Strict);
            api = new DemandController(mock.Object);
            validPetition = new Petition("AAAAAA", "aaaaa", 5);
            //petitions.Add(validPetition);
            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validOwner = new Owner("Paolo", 000111, "rasdas@gmail.com", "$$$aaa123.", "addressR", RoleUser.Owner, "13/09/2022", validPharmacy1);

            validSession1 = new Session("DDIQDS", validEmployee.Name, "4de12a");
            validSession2 = new Session("DAIQDS", validOwner.Name, "4dd12a");
            validDemand = new Demand("BBBBBB", DemandStatus.InProgress);
            validDemand.Petitions.Add(validPetition);
            demands = new List<Demand>() { validDemand };
        }

        [TestMethod]
        public void GetDemandsOk()
        {
            mock.Setup(x => x.GetDemands("4dd12a")).Returns(demands);

            var result = api.GetDemands("4dd12a");
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Demand>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(demands));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetDemandsFail()
        {
            mock.Setup(x => x.GetDemands("4dd12a")).Throws(new Exception());

            var result = api.GetDemands("4dd12a");
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void PostDemandBadRequest()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>(), "4de12a")).Throws(new DemandException());
            var result = api.PostDemand(It.IsAny<Demand>(), "4de12a");
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PostDemandFail()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>(), "4de12a")).Throws(new Exception());
            var result = api.PostDemand(It.IsAny<Demand>(), ("4de12a"));
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostDemandOk()
        {
            mock.Setup(x => x.InsertDemand(It.IsAny<Demand>(), "4de12a")).Returns(validDemand.IdDemand);
            var result = api.PostDemand(It.IsAny<Demand>(), ("4de12a"));
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Solicitud " + validDemand.IdDemand + " exitosa.").Equals(body));
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void PutDemandBadRequest()
        {
            mock.Setup(x => x.UpdateDemand(validDemand.IdDemand, validDemand, validSession1.Token)).Throws(new DemandException());
            var result = api.PutDemand(validDemand.IdDemand, validDemand, validSession1.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void PutDemandNotFound()
        {
            mock.Setup(x => x.UpdateDemand(validDemand.IdDemand, validDemand, validSession1.Token)).Throws(new NotFoundException());
            var result = api.PutDemand(validDemand.IdDemand, validDemand, validSession1.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PutDemandFail()
        {
            mock.Setup(x => x.UpdateDemand(validDemand.IdDemand, validDemand, validSession1.Token)).Throws(new Exception());
            var result = api.PutDemand(validDemand.IdDemand, validDemand, validSession1.Token);
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
            mock.Setup(x => x.UpdateDemand(validDemandModified.IdDemand, validDemandModified, validSession1.Token)).Returns(validDemandModified.IdDemand);
            var result = api.PutDemand(validDemand.IdDemand, validDemandModified, validSession1.Token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(("Modificación de la solicitud: " + validDemandModified.IdDemand + " exitosa.").Equals(body));
        }
    }
}
