using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class MedicineManagementTest
    {
        private Medicine medicine;

        private Session session;
        private Employee employee;
        private Pharmacy validPharmacy1;

        private List<Medicine> medicines;

        [TestInitialize]
        public void InitTest()
        {
            medicine = new Medicine("HUS56A", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Comprimidos, 0, "0.5 ml", 300, false, true);
            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");

            employee = new Employee("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022", validPharmacy1);
            session = new Session("123456", "Lucas", "XXYYZZ");

            medicines = new List<Medicine> { medicine };
        }

        [TestMethod]
        public void InsertMedicineOk()
        {
            ObgContext context = CreateContext();
            IMedicineManagement medicineManagement = new MedicineManagement(context);

            medicineManagement.InsertMedicine(medicine, session);

            Medicine medicineInDatabase = context.Medicines.Where<Medicine>(p => p.Code.Equals(medicine.Code)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(medicineInDatabase);
            Assert.AreEqual(medicineInDatabase.Code, medicine.Code);
        }

        [TestMethod]
        public void GetMedicinesOk()
        {
            IMedicineManagement medicineManagement = CreateMedicineManagement();
            IEnumerable<Medicine> medicinesInDatabase = medicineManagement.GetMedicines();

            Assert.AreEqual(medicinesInDatabase.ToList().Count, medicines.Count);
            Assert.AreEqual(medicinesInDatabase.ToList()[0].Code, medicines[0].Code);

        }

        [TestMethod]
        public void GetMedicineByCodeOk()
        {
            ObgContext context = CreateContext();
            IMedicineManagement medicineManagement = new MedicineManagement(context);

            context.Medicines.Add(medicine);
            context.SaveChanges();

            Medicine medicineInDatabase = medicineManagement.GetMedicineByCode(medicine.Code);

            Assert.IsNotNull(medicineInDatabase);
            Assert.AreEqual(medicineInDatabase.Code, medicine.Code);
        }

        [TestMethod]
        public void UpdateMedicineOk()
        {
            ObgContext context = CreateContext();
            IMedicineManagement medicineManagement = new MedicineManagement(context);

            context.Medicines.Add(medicine);
            context.SaveChanges();
            medicine.Name = "Remedio";
            medicineManagement.UpdateMedicine(medicine);

            Medicine medicineInDatabase = context.Medicines.Where<Medicine>(m => m.Code.Equals(medicine.Code)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(medicineInDatabase);
            Assert.AreEqual(medicineInDatabase.Name, medicine.Name);
        }

        [TestMethod]
        public void DeleteMedicineOk()
        {
            ObgContext context = CreateContext();
            IMedicineManagement medicineManagement = new MedicineManagement(context);

            context.Medicines.Add(medicine);
            context.SaveChanges();

            medicineManagement.DeleteMedicine(medicine);

            Medicine medicineInDatabase = context.Medicines.Where<Medicine>(m => m.Code.Equals(medicine.Code)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(medicineInDatabase);
        }

        private ObgContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ObgContext>()
                .UseInMemoryDatabase("ObgDA2")
                .Options;

            var context = new ObgContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        private IMedicineManagement CreateMedicineManagement()
        {
            var context = CreateContext();

            context.Medicines.Add(medicine);
            context.SaveChanges();

            IMedicineManagement medicineManagement = new MedicineManagement(context);
            return medicineManagement;
        }

    }
}
