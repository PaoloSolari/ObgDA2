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
    }
}
