using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class EmployeeManagementTest
    {
        private Owner owner;
        private Pharmacy pharmacy;
        private Employee employee;
        private List<Employee> employees;

        [TestInitialize]
        public void InitTest()
        {
            owner = new Owner("Julio", 123456, "julio@gmail.com", "abccdefg.123", "Ejido", RoleUser.Owner, "29/09/2022", null);
            pharmacy = new Pharmacy("FarmaShop", "18 de Julio", owner);
            employee = new Employee("Jaime", 232323, "jaime@gmail.com", ".egudhu345d", "18 de Julio", RoleUser.Employee, "29/09/2022", pharmacy);
        
            employees = new List<Employee> { employee };
        }

        [TestMethod]
        public void InsertEmployeeOk()
        {
            ObgContext context = CreateContext();
            IEmployeeManagement employeeManagement = new EmployeeManagement(context);

            employeeManagement.InsertEmployee(employee);

            Employee employeeInDatabase = context.Employees.Where<Employee>(e => e.Name == employee.Name).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(employeeInDatabase);
            Assert.AreEqual(employeeInDatabase.Name, employee.Name);
        }

        private ObgContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ObgContext>()
                .UseInMemoryDatabase("ObgDA2")
                .Options;

            var context = new ObgContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

    }
}
