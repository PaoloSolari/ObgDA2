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
        private List<Medicine> medicines;

        [TestInitialize]
        public void InitTest()
        {
            medicine = new Medicine("HUS56A", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Comprimidos, 0, "0.5 ml", 300, false, true);
            
            medicines = new List<Medicine> { medicine };
        }

        [TestMethod]
        public void InsertMedicineOk()
        {
            ObgContext context = CreateContext();
            IMedicineManagement medicineManagement = new MedicineManagement(context);

            medicineManagement.InsertMedicine(medicine);

            Medicine medicineInDatabase = context.Medicines.Where<Medicine>(p => p.Code == medicine.Code).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(medicineInDatabase);
            Assert.AreEqual(medicineInDatabase.Code, medicine.Code);
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
    }
}
