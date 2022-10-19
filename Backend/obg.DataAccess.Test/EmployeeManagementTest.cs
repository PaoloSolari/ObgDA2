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
            pharmacy = new Pharmacy("FarmaShop", "18 de Julio");
            employee = new Employee("Jaime", 232323, "jaime@gmail.com", ".egudhu345d", "18 de Julio", RoleUser.Employee, "29/09/2022", pharmacy);
        
            employees = new List<Employee> { employee };
        }

        [TestMethod]
        public void InsertEmployeeOk()
        {
            ObgContext context = CreateContext();
            IEmployeeManagement employeeManagement = new EmployeeManagement(context);

            employeeManagement.InsertEmployee(employee);

            Employee employeeInDatabase = context.Employees.Where<Employee>(e => e.Name.Equals(employee.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(employeeInDatabase);
            Assert.AreEqual(employeeInDatabase.Name, employee.Name);
        }

        [TestMethod]
        public void GetEmployeesOk()
        {
            IEmployeeManagement employeeManagement = CreateEmployeeManagement();
            IEnumerable<Employee> employeesInDatabase = employeeManagement.GetEmployees();

            Assert.AreEqual(employeesInDatabase.ToList().Count, employees.Count);
            Assert.AreEqual(employeesInDatabase.ToList()[0].Name, employees[0].Name);

        }

        [TestMethod]
        public void GetEmployeeByNameOk()
        {
            ObgContext context = CreateContext();
            IEmployeeManagement employeeManagement = new EmployeeManagement(context);

            context.Employees.Add(employee);
            context.SaveChanges();

            Employee employeeInDatabase = employeeManagement.GetEmployeeByName(employee.Name);

            Assert.IsNotNull(employeeInDatabase);
            Assert.AreEqual(employeeInDatabase.Name, employee.Name);
        }

        [TestMethod]
        public void UpdateEmployeeOk()
        {
            ObgContext context = CreateContext();
            IEmployeeManagement employeeManagement = new EmployeeManagement(context);

            context.Employees.Add(employee);
            context.SaveChanges();
            employee.Address = "25 de Agosto";
            employeeManagement.UpdateEmployee(employee);

            Employee employeeInDatabase = context.Employees.Where<Employee>(e => e.Name.Equals(employee.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(employeeInDatabase);
            Assert.AreEqual(employeeInDatabase.Address, employee.Address);
        }

        [TestMethod]
        public void DeleteEmployeeOk()
        {
            ObgContext context = CreateContext();
            IEmployeeManagement employeeManagement = new EmployeeManagement(context);

            context.Employees.Add(employee);
            context.SaveChanges();

            employeeManagement.DeleteEmployee(employee);

            Employee employeeInDatabase = context.Employees.Where<Employee>(e => e.Name.Equals(employee.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(employeeInDatabase);
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

        private IEmployeeManagement CreateEmployeeManagement()
        {
            var context = CreateContext();

            context.Employees.Add(employee);
            context.SaveChanges();

            IEmployeeManagement employeeManagement = new EmployeeManagement(context);
            return employeeManagement;
        }

    }
}
