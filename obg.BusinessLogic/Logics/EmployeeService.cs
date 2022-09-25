using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class EmployeeService : UserService, IEmployeeService
    {
        //private readonly IEmployeeManagement _employeeManagement;

        //public EmployeeService(IEmployeeManagement employeeManagement)
        //{
        //    _employeeManagement = employeeManagement;
        //}

        public Employee InsertEmployee(Employee employee)
        {
            if (IsUserValid(employee) && HasAPharmacy(employee) && IsAEmployee(employee))
            {
                // Se agreaga el Employee a la DB: _employeeManagement.InsertEmployee(employee);
                fakeDB.Add(employee);
            }
            return employee;
        }

        private bool HasAPharmacy(Employee employee)
        {
            if(employee.Pharmacy == null)
            {
                throw new UserException("El empleado no tiene una farmacia asignada.");
            }
            return true; ;
        }

        private bool IsAEmployee(Employee employee)
        {
            if(employee.Role != RoleUser.Employee)
            {
                throw new UserException("El empleado tiene asignado un rol incorrecto.");
            }
            return true;
        }

        public IEnumerable<User> GetEmployees()
        {
            return fakeDB;
        }

    }
}
