using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IEmployeeManagement
    {
        void InsertEmployee(Employee employee);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeByName(string name);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

    }
}
