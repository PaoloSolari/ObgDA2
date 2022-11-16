using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
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

        public IEnumerable<Demand> GetDemands(Session session)
        {
            string ownerName = session.UserName;
            Owner ownerSeeDemand = ObgContext.Owners.Where<Owner>(a => a.Name.Equals(ownerName)).Include("Pharmacy").FirstOrDefault();
            if (ownerSeeDemand != null)
            {
                Pharmacy pharmacyOfOwner = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(ownerSeeDemand.Pharmacy.Name)).Include("Demands.Petitions").FirstOrDefault();
                if (pharmacyOfOwner != null)
                {
                    List<Demand> demands = new List<Demand>();
                    foreach (Demand demand  in pharmacyOfOwner.Demands)
                    {
                        if(demand.Status == DemandStatus.InProgress)
                        {
                            demands.Add(demand);
                        }
                    }
                    if(demands.Count > 0)
                    {
                        return demands;
                    }
                }
            }
            return null;
        }

        public Demand GetDemandById(string id)
        {
            return ObgContext.Demands.Where<Demand>(d => d.IdDemand == id).Include("Petitions").FirstOrDefault();
        }

        public void UpdateDemand(Demand demand)
        {
            if(demand.Status == DemandStatus.Accepted)
            {
                foreach (Petition petition in demand.Petitions)
                {
                    string codeMedicine = petition.MedicineCode;
                    Medicine medicineToModify = ObgContext.Medicines.Where<Medicine>(m => m.Code == codeMedicine).FirstOrDefault();
                    if(medicineToModify != null)
                    {
                        medicineToModify.Stock+= petition.NewQuantity;
                        ObgContext.Medicines.Attach(medicineToModify);
                        ObgContext.Entry(medicineToModify).State = EntityState.Modified;
                        ObgContext.SaveChanges();
                    }
                }
            }
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
            Demand demand = ObgContext.Demands.Where<Demand>(d => d.IdDemand == id).FirstOrDefault();
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