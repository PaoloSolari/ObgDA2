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
                // Se agrega el Employee a la DB: _employeeManagement.InsertEmployee(employee);
                FakeDB.Employees.Add(employee);
                FakeDB.Users.Add(employee);
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
            return FakeDB.Employees;
        }

        public Employee UpdateEmployee(Employee employeeToUpdate)
        {
            Employee employee = GetEmployeeByName(employeeToUpdate.Name);
            return employee;
        }

        public Employee GetEmployeeByName(string name)
        {

            Employee auxEmployee = null;
            foreach (Employee employee in FakeDB.Employees)
            {
                if (employee.Name.Equals(name))
                {
                    auxEmployee = employee;
                }
            }
            if (auxEmployee == null)
            {
                throw new UserException("El administrador no existe.");
            }
            return auxEmployee;
        }

    }
}
