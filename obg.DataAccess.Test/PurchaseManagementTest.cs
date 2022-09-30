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
    public class PurchaseManagementTest
    {
        private Purchase purchase;
        private List<Purchase> purchases;

        [TestInitialize]
        public void InitTest()
        {
            purchase = new Purchase("ANISHU", 678, "pepe@gmail.com");

            purchases = new List<Purchase>() { purchase };
        }

        [TestMethod]
        public void InsertPurchaseOk()
        {
            ObgContext context = CreateContext();
            IPurchaseManagement pharmacyManagement = new PurchaseManagement(context);

            pharmacyManagement.InsertPurchase(purchase);

            Purchase purchaseInDatabase = context.Purchases.Where<Purchase>(p => p.IdPurchase == purchase.IdPurchase).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(purchaseInDatabase);
            Assert.AreEqual(purchaseInDatabase.IdPurchase, purchase.IdPurchase);
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
