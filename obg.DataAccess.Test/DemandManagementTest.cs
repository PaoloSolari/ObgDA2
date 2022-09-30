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

        [TestInitialize]
        public void InitTest()
        {
            demand = new Demand("AABBCC", DemandStatus.Accepted);
            
            demands = new List<Demand> { demand };
        }

        [TestMethod]
        public void InsertDemandOk()
        {
            ObgContext context = CreateContext();
            IDemandManagement demandManagement = new DemandManagement(context);

            demandManagement.InsertDemand(demand);

            Demand demandInDatabase = context.Demands.Where<Demand>(d => d.IdDemand.Equals(demand.IdDemand)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(demandInDatabase);
            Assert.AreEqual(demandInDatabase.IdDemand, demand.IdDemand);
        }

        [TestMethod]
        public void GetDemandsOk()
        {
            IDemandManagement demandManagement = CreateDemandManagement();
            IEnumerable<Demand> demandsInDatabase = demandManagement.GetDemands();

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
