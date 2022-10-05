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

        private Administrator administrator;
        private Session session;

        [TestInitialize]
        public void InitTest()
        {
            owner = new Owner("Julio", 123456, "julio@gmail.com", "abccdefg.123", "Ejido", RoleUser.Owner, "29/09/2022", null);
            pharmacy = new Pharmacy("FarmaShop", "18 de Julio");

            administrator = new Administrator("Lucas", 000102, "l@gmail.com", "###bbb123.", "addressL", RoleUser.Employee, "13/09/2022");
            //administrator.Pharmacies.Add(pharmacy);
            session = new Session("123456", "Lucas", "XXYYZZ");

            pharmacies = new List<Pharmacy>() { pharmacy };
        }

        [TestMethod]
        public void InsertPharmacyOk()
        {
            ObgContext context = CreateContext();
            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);
      
            pharmacyManagement.InsertPharmacy(pharmacy, session);

            Pharmacy pharmacyInDatabase = context.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(pharmacy.Name)).AsNoTracking().FirstOrDefault();

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

        [TestMethod]
        public void GetPharmacyByNameOk()
        {
            ObgContext context = CreateContext();
            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);

            context.Pharmacies.Add(pharmacy);
            context.SaveChanges();

            Pharmacy pharmacyInDatabase = pharmacyManagement.GetPharmacyByName(pharmacy.Name);

            Assert.IsNotNull(pharmacyInDatabase);
            Assert.AreEqual(pharmacyInDatabase.Name, pharmacy.Name);
        }

        [TestMethod]
        public void UpdatePharmacyOk()
        {
            ObgContext context = CreateContext();
            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);

            context.Pharmacies.Add(pharmacy);
            context.SaveChanges();
            pharmacy.Address = "25 de Agosto";
            pharmacyManagement.UpdatePharmacy(pharmacy);

            Pharmacy pharmacyInDatabase = context.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(pharmacy.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(pharmacyInDatabase);
            Assert.AreEqual(pharmacyInDatabase.Address, pharmacy.Address);
        }

        [TestMethod]
        public void DeletePharmacyOk()
        {
            ObgContext context = CreateContext();
            IPharmacyManagement pharmacyManagement = new PharmacyManagement(context);

            context.Pharmacies.Add(pharmacy);
            context.SaveChanges();

            pharmacyManagement.DeletePharmacy(pharmacy);

            Pharmacy pharmacyInDatabase = context.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(pharmacy.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(pharmacyInDatabase);
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
