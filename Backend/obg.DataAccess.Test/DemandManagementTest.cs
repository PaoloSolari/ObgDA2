using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class DemandManagementTest
    {
        private Demand demand;
        private List<Demand> demands;
        private Pharmacy validPharmacy1;

        private Session validSession1;
        private Employee validEmployee;
        private Owner validOwner;

        [TestInitialize]
        public void InitTest()
        {
            demand = new Demand("AABBCC", DemandStatus.InProgress);
            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validOwner = new Owner("Paolo", 000111, "rasdas@gmail.com", "$$$aaa123.", "addressR", RoleUser.Owner, "13/09/2022", validPharmacy1);

            validSession1 = new Session("DDIQDS", validOwner.Name, "4de12a");
            demands = new List<Demand> { demand };
            validPharmacy1.Demands = demands;
        }

        [TestMethod]
        public void InsertDemandOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            demandManagement.InsertDemand(demand, validSession1);

            Demand demandInDatabase = context.Demands.Where<Demand>(d => d.IdDemand.Equals(demand.IdDemand)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(demandInDatabase);
            Assert.AreEqual(demandInDatabase.IdDemand, demand.IdDemand);
        }

        [TestMethod]
        public void GetDemandsOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            context.Demands.Add(demand);
            context.Owners.Add(validOwner);
            context.SaveChanges();
            IEnumerable<Demand> demandsInDatabase = demandManagement.GetDemands(validSession1);

            Assert.AreEqual(demandsInDatabase.ToList().Count, demands.Count);
            Assert.AreEqual(demandsInDatabase.ToList()[0].IdDemand, demands[0].IdDemand);

        }

        [TestMethod]
        public void GetDemandByIdOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            context.Demands.Add(demand);
            context.SaveChanges();

            Demand demandInDatabase = demandManagement.GetDemandById(demand.IdDemand);

            Assert.IsNotNull(demandInDatabase);
            Assert.AreEqual(demandInDatabase.IdDemand, demand.IdDemand);
        }

        [TestMethod]
        public void UpdateDemandOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            context.Demands.Add(demand);
            context.SaveChanges();
            demand.Status = DemandStatus.Rejected;
            demandManagement.UpdateDemand(demand);

            Demand demandInDatabase = context.Demands.Where<Demand>(d => d.IdDemand.Equals(demand.IdDemand)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(demandInDatabase);
            Assert.AreEqual(demandInDatabase.Status, demand.Status);
        }

        [TestMethod]
        public void DeleteDemandOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            context.Demands.Add(demand);
            context.SaveChanges();

            demandManagement.DeleteDemand(demand);

            Demand demandInDatabase = context.Demands.Where<Demand>(d => d.IdDemand.Equals(demand.IdDemand)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(demandInDatabase);
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

        private IDemandManagement CreateDemandManagement()
        {
            var context = CreateContext();

            context.Demands.Add(demand);
            context.SaveChanges();

            IDemandManagement demandManagement = new DemandManagement(context);
            return demandManagement;
        }

    }
}
