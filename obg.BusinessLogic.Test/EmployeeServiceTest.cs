using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using Moq;
using System.Collections;
using System.Collections.Generic;
using obg.BusinessLogic.Logics;
using obg.Exceptions;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeManagement> mock;
        private Employee validEmployee1;
        private Employee validEmployee2;
        private Pharmacy pharmacy1;
        private Pharmacy pharmacy2;
        private Employee nullEmployee;
        private EmployeeService service;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IEmployeeManagement>(MockBehavior.Strict);
            service = new EmployeeService(mock.Object);
            pharmacy1 = new Pharmacy("San Roque", "aaaa", null);
            pharmacy2 = new Pharmacy("Farmacity", "aaaa", null);
            validEmployee1 = new Employee("Rodrigo", "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Employee, "13/09/2022", pharmacy1, null);
            validEmployee2 = new Employee("Lucas", "lr@gmail.com", "###bbb123.", "address", RoleUser.Employee, "13/09/2022", pharmacy2, null);
            pharmacy1.AddEmployee(validEmployee1);
            pharmacy2.AddEmployee(validEmployee2);
            nullEmployee = null;
        }

        [TestMethod]
        public void InsertEmployeeOK()
        {
            service.InsertEmployee(validEmployee1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullEmployee()
        {
            service.InsertEmployee(nullEmployee);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullName()
        {
            validEmployee1.Name = null;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmptyName()
        {
            validEmployee1.Name = "";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RepeatedName()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Name = "Rodrigo";
            service.InsertEmployee(validEmployee2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameHasMore20Chars()
        {
            validEmployee1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullCode()
        {
            validEmployee1.Code = null;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmptyCode()
        {
            validEmployee1.Code = "";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RepeatedCode()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Code = validEmployee1.Code;
            service.InsertEmployee(validEmployee2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullEmail()
        {
            validEmployee1.Email = null;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmptyEmail()
        {
            validEmployee1.Email = "";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RepeatedEmail()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Email = "rp@gmail.com";
            service.InsertEmployee(validEmployee2);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailHasNoFormat()
        {
            validEmployee1.Email = "psgmail.com";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordHasLess8Chars()
        {
            validEmployee1.Password = "aab#bcc";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordHasNoSpecialChar()
        {
            validEmployee1.Password = "aabbccdd";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullAddress()
        {
            validEmployee1.Address = null;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RoleIncorrectOwner()
        {
            validEmployee1.Role = RoleUser.Owner;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RoleIncorrectAdmnistrator()
        {
            validEmployee1.Role = RoleUser.Administrator;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullRegisterDate()
        {
            validEmployee1.RegisterDate = null;
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmptyRegisterDate()
        {
            validEmployee1.RegisterDate = "";
            service.InsertEmployee(validEmployee1);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NullPharmacy()
        {
            validEmployee1.Pharmacy = null;
            service.InsertEmployee(validEmployee1);
        }

    }
}
