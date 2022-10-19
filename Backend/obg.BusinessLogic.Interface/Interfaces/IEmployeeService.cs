using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<User> GetEmployees();
        string InsertEmployee(Employee employee);
        string UpdateEmployee(Employee employee);
    }
}
