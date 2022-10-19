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
            IPurchaseManagement purchaseManagement = new PurchaseManagement(context);

            purchaseManagement.InsertPurchase(purchase);

            Purchase purchaseInDatabase = context.Purchases.Where<Purchase>(p => p.IdPurchase.Equals(purchase.IdPurchase)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(purchaseInDatabase);
            Assert.AreEqual(purchaseInDatabase.IdPurchase, purchase.IdPurchase);
        }

        [TestMethod]
        public void GetPurchasesOk()
        {
            IPurchaseManagement purchaseManagement = CreatePurchaseManagement();
            IEnumerable<Purchase> purchasesInDatabase = purchaseManagement.GetPurchases();

            Assert.AreEqual(purchasesInDatabase.ToList().Count, purchases.Count);
            Assert.AreEqual(purchasesInDatabase.ToList()[0].IdPurchase, purchases[0].IdPurchase);

        }

        [TestMethod]
        public void GetPurchaseByIdOk()
        {
            ObgContext context = CreateContext();
            IPurchaseManagement purchaseManagement = new PurchaseManagement(context);

            context.Purchases.Add(purchase);
            context.SaveChanges();

            Purchase purchaseInDatabase = purchaseManagement.GetPurchaseById(purchase.IdPurchase);

            Assert.IsNotNull(purchaseInDatabase);
            Assert.AreEqual(purchaseInDatabase.IdPurchase, purchase.IdPurchase);
        }

        [TestMethod]
        public void UpdatePurchaseOk()
        {
            ObgContext context = CreateContext();
            IPurchaseManagement purchaseManagement = new PurchaseManagement(context);

            context.Purchases.Add(purchase);
            context.SaveChanges();
            purchase.Amount = 800;
            purchaseManagement.UpdatePurchase(purchase);

            Purchase purchaseInDatabase = context.Purchases.Where<Purchase>(p => p.IdPurchase.Equals(purchase.IdPurchase)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(purchaseInDatabase);
            Assert.AreEqual(purchaseInDatabase.Amount, purchase.Amount);
        }

        [TestMethod]
        public void DeletePurchaseOk()
        {
            ObgContext context = CreateContext();
            IPurchaseManagement purchaseManagement = new PurchaseManagement(context);

            context.Purchases.Add(purchase);
            context.SaveChanges();

            purchaseManagement.DeletePurchase(purchase);

            Purchase purchaseInDatabase = context.Purchases.Where<Purchase>(p => p.IdPurchase.Equals(purchase.IdPurchase)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(purchaseInDatabase);
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

        private IPurchaseManagement CreatePurchaseManagement()
        {
            var context = CreateContext();

            context.Purchases.Add(purchase);
            context.SaveChanges();

            IPurchaseManagement purchaseManagement = new PurchaseManagement(context);
            return purchaseManagement;
        }
    }
}
