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
        private readonly IEmployeeManagement _employeeManagement;

        public EmployeeService(IEmployeeManagement employeeManagement)
        {
            _employeeManagement = employeeManagement;
        }

        public Employee InsertEmployee(Employee employee)
        {
            if (IsUserValid(employee) && HasAPharmacy(employee) && IsAnEmployee(employee))
            {
                // Se agrega el Employee a la DB: _employeeManagement.InsertEmployee(employee);
                _employeeManagement.InsertEmployee(employee);
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

        private bool IsAnEmployee(Employee employee)
        {
            if(employee.Role != RoleUser.Employee)
            {
                throw new UserException("El empleado tiene asignado un rol incorrecto.");
            }
            return true;
        }

        public IEnumerable<User> GetEmployees()
        {
            return _employeeManagement.GetEmployees();
        }

        public Employee UpdateEmployee(Employee employeeToUpdate)
        {
            if (IsUserValid(employeeToUpdate) && HasAPharmacy(employeeToUpdate) && IsAnEmployee(employeeToUpdate))
            {
                Employee employee = _employeeManagement.GetEmployeeByName(employeeToUpdate.Name);
                if (employee == null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }
                _employeeManagement.UpdateEmployee(employeeToUpdate);
            }
            return employeeToUpdate;
        }

        public Employee GetEmployeeByName(string name)
        {

            Employee employee = _employeeManagement.GetEmployeeByName(name);
            if (employee == null)
            {
                throw new NotFoundException("El empleado no existe");
            }
            return employee;
        }

    }
}
