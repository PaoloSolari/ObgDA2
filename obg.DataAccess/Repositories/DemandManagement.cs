using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace obg.DataAccess.Repositories
{
    public class DemandManagement : IDemandManagement
    {
        private ObgContext ObgContext { get; set; }
        public DemandManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertDemand(Demand demand, Session session)
        {
            string employeeName = session.UserName;
            Employee employeeCreatingDemand = ObgContext.Employees.Where<Employee>(a => a.Name.Equals(employeeName)).Include("Pharmacy").FirstOrDefault();
            if (employeeCreatingDemand != null)
            {
                Pharmacy pharmacyOfEmployee = ObgContext.Pharmacies.Where<Pharmacy>(a => a.Name.Equals(employeeCreatingDemand.Pharmacy.Name)).Include("Demands").FirstOrDefault();
                if (pharmacyOfEmployee != null)
                {
                    pharmacyOfEmployee.Demands.Add(demand);
                    ObgContext.Attach(pharmacyOfEmployee);
                }
            }

            ObgContext.Demands.Add(demand);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Demand> GetDemands()
        {
            return ObgContext.Demands.ToList();
        }

        public Demand GetDemandById(string id)
        {
            return ObgContext.Demands.Where<Demand>(d => d.IdDemand == id).AsNoTracking().FirstOrDefault();
        }

        public void UpdateDemand(Demand demand)
        {
            ObgContext.Demands.Attach(demand);
            ObgContext.Entry(demand).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteDemand(Demand demand)
        {
            ObgContext.Demands.Remove(demand);
            ObgContext.SaveChanges();
        }

        public bool DemandExists(string id)
        {
            Demand demand = ObgContext.Demands.Where<Demand>(d => d.IdDemand == id).AsNoTracking().FirstOrDefault();
            if(demand != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}