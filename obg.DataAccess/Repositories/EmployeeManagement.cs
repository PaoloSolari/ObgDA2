using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;

namespace obg.DataAccess.Repositories
{
    public class EmployeeManagement : IEmployeeManagement
    {
        private ObgContext ObgContext { get; set; }
        public EmployeeManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertEmployee(Employee employee)
        {
            ObgContext.Employees.Add(employee);
            ObgContext.SaveChanges();
        }
    }
}