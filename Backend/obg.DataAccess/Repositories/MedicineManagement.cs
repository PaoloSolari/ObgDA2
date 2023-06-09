﻿using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace obg.DataAccess.Repositories
{
    public class MedicineManagement : IMedicineManagement
    {
        private ObgContext ObgContext { get; set; }
        public MedicineManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertMedicine(Medicine medicine, Session session)
        {
            string employeeName = session.UserName;
            Employee employeeCreatingMedicine = ObgContext.Employees.Where<Employee>(a => a.Name.Equals(employeeName)).Include("Pharmacy").FirstOrDefault();
            if (employeeCreatingMedicine != null)
            {
                Pharmacy pharmacyOfEmployee = ObgContext.Pharmacies.Where<Pharmacy>(a => a.Name.Equals(employeeCreatingMedicine.Pharmacy.Name)).Include("Medicines").FirstOrDefault();
                if (pharmacyOfEmployee != null)
                {
                    pharmacyOfEmployee.Medicines.Add(medicine);
                    ObgContext.Attach(pharmacyOfEmployee);
                }
            }

            ObgContext.Medicines.Add(medicine);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            return ObgContext.Medicines.ToList();
        }

        public Medicine GetMedicineByCode(string code)
        {
            return ObgContext.Medicines.Where<Medicine>(m => m.Code == code).FirstOrDefault();
        }

        public void UpdateMedicine(Medicine medicine)
        {
            ObgContext.Medicines.Attach(medicine);
            ObgContext.Entry(medicine).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteMedicine(Medicine medicine)
        {
            ObgContext.Medicines.Remove(medicine);
            ObgContext.SaveChanges();
        }

        public bool IsCodeRegistered(string code)
        {
            Medicine medicine = ObgContext.Medicines.Where<Medicine>(m => m.Code.Equals(code)).FirstOrDefault();
            if(medicine != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Medicine> GetMedicinesByName(string name)
        {
            return ObgContext.Medicines.ToList().Where<Medicine>(m => m.Name.Equals(name));
        }
    }
}