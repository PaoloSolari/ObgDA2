﻿using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Employee> GetEmployees()
        {
            return ObgContext.Employees.ToList();
        }

        public Employee GetEmployeeByName(string name)
        {
            return ObgContext.Employees.Where<Employee>(e => e.Name == name).AsNoTracking().FirstOrDefault();
        }

        public void UpdateEmployee(Employee employee)
        {
            ObgContext.Employees.Attach(employee);
            ObgContext.Entry(employee).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            ObgContext.Employees.Remove(employee);
            ObgContext.SaveChanges();
        }
    }
}