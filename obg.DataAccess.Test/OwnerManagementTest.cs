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
    public class OwnerManagementTest
    {
        private Owner owner;
        private List<Owner> owners;

        [TestInitialize]
        public void InitTest()
        {
            owner = new Owner("Julio", 123456, "julio@gmail.com", "abccdefg.123", "Ejido", RoleUser.Owner, "29/09/2022", null);
        
            owners = new List<Owner>() { owner };
        }

        [TestMethod]
        public void InsertOwnerOk()
        {
            ObgContext context = CreateContext();
            IOwnerManagement ownerManagement = new OwnerManagement(context);

            ownerManagement.InsertOwner(owner);

            Owner ownerInDatabase = context.Owners.Where<Owner>(p => p.Name == owner.Name).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(ownerInDatabase);
            Assert.AreEqual(ownerInDatabase.Name, owner.Name);
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
