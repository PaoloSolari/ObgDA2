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

namespace obg.DataAccess.Test
{
    [TestClass]
    public class PurchaseLineManagementTest
    {
        private PurchaseLine purchaseLine;
        private List<PurchaseLine> purchaseLines;

        [TestInitialize]
        public void InitTest()
        {
            purchaseLine = new PurchaseLine("KOAISA", "89SYDG", 56);

            purchaseLines = new List<PurchaseLine> { purchaseLine };
        }

        [TestMethod]
        public void InsertPurchaseLineOk()
        {
            ObgContext context = CreateContext();
            IPurchaseLineManagement purchaseLineManagement = new PurchaseLineManagement(context);

            purchaseLineManagement.InsertPurchaseLine(purchaseLine);

            PurchaseLine purchaseLineInDatabase = context.PurchaseLines.Where<PurchaseLine>(pl => pl.IdPurchaseLine.Equals(purchaseLine.IdPurchaseLine)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(purchaseLineInDatabase);
            Assert.AreEqual(purchaseLineInDatabase.IdPurchaseLine, purchaseLine.IdPurchaseLine);
        }

        [TestMethod]
        public void GetPurchaseLinesOk()
        {
            IPurchaseLineManagement purchaseLineManagement = CreatePurchaseLineManagement();
            IEnumerable<PurchaseLine> purchaseLinesInDatabase = purchaseLineManagement.GetPurchaseLines();

            Assert.AreEqual(purchaseLinesInDatabase.ToList().Count, purchaseLines.Count);
            Assert.AreEqual(purchaseLinesInDatabase.ToList()[0].IdPurchaseLine, purchaseLines[0].IdPurchaseLine);

        }

        [TestMethod]
        public void GetPurchaseLineByIdOk()
        {
            ObgContext context = CreateContext();
            IPurchaseLineManagement purchaseLineManagement = new PurchaseLineManagement(context);

            context.PurchaseLines.Add(purchaseLine);
            context.SaveChanges();

            PurchaseLine purchaseLineInDatabase = purchaseLineManagement.GetPurchaseLineById(purchaseLine.IdPurchaseLine);

            Assert.IsNotNull(purchaseLineInDatabase);
            Assert.AreEqual(purchaseLineInDatabase.IdPurchaseLine, purchaseLine.IdPurchaseLine);
        }

        [TestMethod]
        public void UpdatePurchaseLineOk()
        {
            ObgContext context = CreateContext();
            IPurchaseLineManagement purchaseLineManagement = new PurchaseLineManagement(context);

            context.PurchaseLines.Add(purchaseLine);
            context.SaveChanges();
            purchaseLine.MedicineQuantity = 30;
            purchaseLineManagement.UpdatePurchaseLine(purchaseLine);

            PurchaseLine purchaseLineInDatabase = context.PurchaseLines.Where<PurchaseLine>(pl => pl.IdPurchaseLine.Equals(purchaseLine.IdPurchaseLine)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(purchaseLineInDatabase);
            Assert.AreEqual(purchaseLineInDatabase.MedicineQuantity, purchaseLine.MedicineQuantity);
        }

        [TestMethod]
        public void DeletePurchaseLineOk()
        {
            ObgContext context = CreateContext();
            IPurchaseLineManagement purchaseLineManagement = new PurchaseLineManagement(context);

            context.PurchaseLines.Add(purchaseLine);
            context.SaveChanges();

            purchaseLineManagement.DeletePurchaseLine(purchaseLine);

            PurchaseLine purchaseLineInDatabase = context.PurchaseLines.Where<PurchaseLine>(pl => pl.IdPurchaseLine.Equals(purchaseLine.IdPurchaseLine)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(purchaseLineInDatabase);
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

        private IPurchaseLineManagement CreatePurchaseLineManagement()
        {
            var context = CreateContext();

            context.PurchaseLines.Add(purchaseLine);
            context.SaveChanges();

            IPurchaseLineManagement purchaseLineManagement = new PurchaseLineManagement(context);
            return purchaseLineManagement;
        }
    }
}
