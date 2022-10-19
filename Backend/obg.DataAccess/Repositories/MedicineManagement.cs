using Microsoft.EntityFrameworkCore;
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

        /*
        public void InsertMedicine(Medicine medicine, Session session)
        {
            string employeeName = session.UserName;
            Employee employeeCreatingMedicine = ObgContext.Employees.Where<Employee>(a => a.Name.Equals(employeeName)).Include("Pharmacy").FirstOrDefault();
            if (employeeCreatingMedicine != null)
            {
                Pharmacy pharmacyOfEmployee = ObgContext.Pharmacies.Where<Pharmacy>(a => a.Name.Equals(employeeCreatingMedicine.Pharmacy.Name)).Include("Medicines").FirstOrDefault();
                if (pharmacyOfEmployee != null)
                {
                    int preInsertCount = pharmacyOfEmployee.Medicines.Count;
                    pharmacyOfEmployee.Medicines.Add(medicine);
                    if (preInsertCount == 0) // Nos evita la excepción.
                    {
                        ObgContext.Attach(pharmacyOfEmployee);
                    }
                    else
                    {
                        List<Medicine> updateMedicines = new List<Medicine>();
                        int count = 0;
                        foreach (Medicine copyMedicine in pharmacyOfEmployee.Medicines)
                        {
                            Medicine newMedicine = CloneMedicine(copyMedicine);
                            updateMedicines.Add(newMedicine);
                            count++;
                        }
                        
                        pharmacyOfEmployee.Medicines.RemoveRange(0, count);

                        ObgContext.SaveChanges();

                        Employee employeeCreatingMedicineNOW = ObgContext.Employees.Where<Employee>(a => a.Name.Equals(employeeName)).Include("Pharmacy").AsNoTracking().FirstOrDefault();
                        Pharmacy pharmacyOfEmployeeNOW = ObgContext.Pharmacies.Where<Pharmacy>(a => a.Name.Equals(employeeCreatingMedicine.Pharmacy.Name)).Include("Medicines").AsNoTracking().FirstOrDefault();
                        foreach (Medicine newMedicine in updateMedicines)
                        {
                            pharmacyOfEmployeeNOW.Medicines.Add(newMedicine);
                        }

                        ObgContext.Attach(pharmacyOfEmployeeNOW);
                        ObgContext.SaveChanges();
                    }
                }
            }

            ObgContext.Medicines.Add(medicine);
            ObgContext.SaveChanges();
        }
        
        private Medicine CloneMedicine(Medicine medicine)
        {
            Medicine newMedicine = new Medicine();
            newMedicine.Code = medicine.Code;
            newMedicine.Name = medicine.Name;
            newMedicine.SymtompsItTreats = medicine.SymtompsItTreats;
            newMedicine.Presentation = medicine.Presentation;
            newMedicine.Quantity = medicine.Quantity;
            newMedicine.Unit = medicine.Unit;
            newMedicine.Price = medicine.Price;
            newMedicine.Stock = medicine.Stock;
            newMedicine.Prescription = medicine.Prescription;
            newMedicine.IsActive = medicine.IsActive;
            return newMedicine;
        }
        */

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