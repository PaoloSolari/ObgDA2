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
        private Employee nullEmployee;
        private EmployeeService service;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IEmployeeManagement>(MockBehavior.Strict);
            service = new EmployeeService(mock.Object);
            validEmployee1 = new Employee("Rodrigo", "jijiji", "rp@gmail.com", "$$$aaa123.", "addressPS", RoleUser.Employee, "13/09/2022", null, null);
            validEmployee2 = new Employee("Lucas", "nolozzz", "lr@gmail.com", "###bbb123.", "address", RoleUser.Employee, "13/09/2022", null, null);
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
        public void InsertEmployeeWrong_EmployeeNull()
        {
            service.InsertEmployee(nullEmployee);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameNull()
        {
            validEmployee1.Name = null;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameEmpty()
        {
            validEmployee1.Name = "";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameRepeated()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Name = "Rodrigo";
            service.InsertEmployee(validEmployee2);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_NameHasMore20Chars()
        {
            validEmployee1.Name = "#aaabbbccc$aaabbbcccD";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_CodeNull()
        {
            validEmployee1.Code = null;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_CodeEmpty()
        {
            validEmployee1.Code = "";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_CodeRepeated()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Code = "jijiji";
            service.InsertEmployee(validEmployee2);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailNull()
        {
            validEmployee1.Email = null;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailEmpty()
        {
            validEmployee1.Email = "";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailRepeated()
        {
            service.InsertEmployee(validEmployee1);
            validEmployee2.Email = "rp@gmail.com";
            service.InsertEmployee(validEmployee2);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_EmailHasNoFormat()
        {
            validEmployee1.Email = "psgmail.com";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordHasLess8Chars()
        {
            validEmployee1.Password = "aab#bcc";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_PasswordHasNoSpecialChar()
        {
            validEmployee1.Password = "aabbccdd";
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_AddressNull()
        {
            validEmployee1.Address = null;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RoleIncorrectOwner()
        {
            validEmployee1.Role = RoleUser.Owner;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RoleIncorrectAdmnistrator()
        {
            validEmployee1.Role = RoleUser.Administrator;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RegisterDateNull()
        {
            validEmployee1.RegisterDate = null;
            service.InsertEmployee(validEmployee1);
            //mock.VerifyAll();
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void InsertEmployeeWrong_RegisterDateEmpty()
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
