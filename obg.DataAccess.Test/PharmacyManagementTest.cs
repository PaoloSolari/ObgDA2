using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using obg.DataAccess.Repositories;
using obg.Domain.Enums;
using System.Data;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class PharmacyManagementTest
    {
        private Owner owner;
        private Pharmacy pharmacy;
        private List<Pharmacy> pharmacies;

        [TestInitialize]
        public void InitTest()
        {
            owner = new Owner("Julio", 123456, "julio@gmail.com", "abccdefg.123", "Ejido", RoleUser.Owner, "29/09/2022", null);
            pharmacy = new Pharmacy("FarmaShop", "18 de Julio", owner);

            pharmacies = new List<Pharmacy>() { pharmacy };
        }

        [TestMethod]
        public void InsertPharmacyOk()
        {
            ObgContext context = CreateContext();
            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);
      
            pharmacyManagement.InsertPharmacy(pharmacy);

            Pharmacy pharmacyInDatabase = context.Pharmacies.Where<Pharmacy>(p => p.Name == pharmacy.Name).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(pharmacyInDatabase);
            Assert.AreEqual(pharmacyInDatabase.Name, pharmacy.Name);
        }

        [TestMethod]
        public void GetPharmaciesOk()
        {
            IPharmacyManagement pharmacyManagement = CreatePharmacyManagement();
            IEnumerable<Pharmacy> pharmaciesInDatabase = pharmacyManagement.GetPharmacies();

            Assert.AreEqual(pharmaciesInDatabase.ToList().Count, pharmacies.Count);
            Assert.AreEqual(pharmaciesInDatabase.ToList()[0].Name, pharmacies[0].Name);

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

        private IPharmacyManagement CreatePharmacyManagement()
        {
            var context = CreateContext();

            context.Pharmacies.Add(pharmacy);
            context.SaveChanges();

            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);
            return pharmacyManagement;
        }

    }
}
