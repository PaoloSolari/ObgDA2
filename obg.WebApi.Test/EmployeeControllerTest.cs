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
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeService> mock;
        private EmployeeController api;
        private Employee validEmployee;
        private Pharmacy pharmacy1;
        private IEnumerable<Employee> employees;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IEmployeeService>(MockBehavior.Strict);
            api = new EmployeeController(mock.Object);
            pharmacy1 = new Pharmacy("San Roque", "aaaa", null);
            validEmployee = new Employee("Paolo", "ps@gmail.com", "password123.", "addressPS", RoleUser.Employee, "12/09/2022", pharmacy1, null);
            employees = new List<Employee>() { validEmployee };
        }

        [TestMethod]
        public void GetEmployeesOk()
        {
            mock.Setup(x => x.GetEmployees()).Returns(employees);

            var result = api.GetEmployees();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Employee>;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(body.SequenceEqual(employees));
        }

        [TestMethod]
        public void GetEmployeesFail()
        {
            mock.Setup(x => x.GetEmployees()).Throws(new Exception());

            var result = api.GetEmployees();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostEmployeeBadRequest()
        {
            mock.Setup(x => x.InsertEmployee(It.IsAny<Employee>())).Throws(new UserException());
            var result = api.PostEmployee(It.IsAny<Employee>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PostEmployeeFail()
        {
            mock.Setup(x => x.InsertEmployee(It.IsAny<Employee>())).Throws(new Exception());
            var result = api.PostEmployee(It.IsAny<Employee>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PostEmployeeOk()
        {
            mock.Setup(x => x.InsertEmployee(It.IsAny<Employee>())).Returns(validEmployee);
            var result = api.PostEmployee(It.IsAny<Employee>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Employee;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validEmployee.Equals(body));
        }

        [TestMethod]
        public void PutEmployeeBadRequest()
        {
            mock.Setup(x => x.UpdateEmployee(validEmployee)).Throws(new UserException());
            var result = api.PutEmployee(validEmployee.Name, validEmployee);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void PutEmployeeNotFound()
        {
            mock.Setup(x => x.UpdateEmployee(validEmployee)).Throws(new NotFoundException());
            var result = api.PutEmployee(validEmployee.Name, validEmployee);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void PutEmployeeFail()
        {
            mock.Setup(x => x.UpdateEmployee(validEmployee)).Throws(new Exception());
            var result = api.PutEmployee(validEmployee.Name, validEmployee);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(500, statusCode);
        }

        [TestMethod]
        public void PutEmployeeOk()
        {
            var validEmployeeModified = validEmployee;
            validEmployeeModified.Password = "new password";
            mock.Setup(x => x.UpdateEmployee(validEmployeeModified)).Returns(validEmployeeModified);
            var result = api.PutEmployee(validEmployee.Name, validEmployeeModified);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Employee;

            mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
            Assert.IsTrue(validEmployeeModified.Password.Equals(body.Password));
        }
    }
}
