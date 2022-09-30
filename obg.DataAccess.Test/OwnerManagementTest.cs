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
            owner = new Owner("Julio", 123456, "julio@gmail.com", "abccdefg.123", "18 de Julio", RoleUser.Owner, "29/09/2022", null);
        
            owners = new List<Owner>() { owner };
        }

        [TestMethod]
        public void InsertOwnerOk()
        {
            ObgContext context = CreateContext();
            IOwnerManagement ownerManagement = new OwnerManagement(context);

            ownerManagement.InsertOwner(owner);

            Owner ownerInDatabase = context.Owners.Where<Owner>(p => p.Name.Equals(owner.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(ownerInDatabase);
            Assert.AreEqual(ownerInDatabase.Name, owner.Name);
        }

        [TestMethod]
        public void GetOwnersOk()
        {
            IOwnerManagement ownerManagement = CreateOwnerManagement();
            IEnumerable<Owner> ownersInDatabase = ownerManagement.GetOwners();

            Assert.AreEqual(ownersInDatabase.ToList().Count, owners.Count);
            Assert.AreEqual(ownersInDatabase.ToList()[0].Name, owners[0].Name);

        }

        [TestMethod]
        public void GetOwnerByNameOk()
        {
            ObgContext context = CreateContext();
            IOwnerManagement ownerManagement = new OwnerManagement(context);

            context.Owners.Add(owner);
            context.SaveChanges();

            Owner ownerInDatabase = ownerManagement.GetOwnerByName(owner.Name);

            Assert.IsNotNull(ownerInDatabase);
            Assert.AreEqual(ownerInDatabase.Name, owner.Name);
        }

        [TestMethod]
        public void UpdateOwnerOk()
        {
            ObgContext context = CreateContext();
            IOwnerManagement ownerManagement = new OwnerManagement(context);

            context.Owners.Add(owner);
            context.SaveChanges();
            owner.Address = "25 de Agosto";
            ownerManagement.UpdateOwner(owner);

            Owner ownerInDatabase = context.Owners.Where<Owner>(o => o.Name.Equals(owner.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(ownerInDatabase);
            Assert.AreEqual(ownerInDatabase.Address, owner.Address);
        }

        [TestMethod]
        public void DeleteOwnerOk()
        {
            ObgContext context = CreateContext();
            IOwnerManagement ownerManagement = new OwnerManagement(context);

            context.Owners.Add(owner);
            context.SaveChanges();

            ownerManagement.DeleteOwner(owner);

            Owner ownerInDatabase = context.Owners.Where<Owner>(o => o.Name.Equals(owner.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(ownerInDatabase);
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

        private IOwnerManagement CreateOwnerManagement()
        {
            var context = CreateContext();

            context.Owners.Add(owner);
            context.SaveChanges();

            IOwnerManagement ownerManagement = new OwnerManagement(context);
            return ownerManagement;
        }

    }
}
