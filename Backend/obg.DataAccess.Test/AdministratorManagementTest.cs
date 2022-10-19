using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class AdministratorManagementTest
    {
        private Administrator administrator;
        private List<Administrator> administrators;
        [TestInitialize]
        public void InitTest()
        {
            administrator = new Administrator("Gabriel", 987654, "gabriel@gmail.com", "ahi1244..dd", "18 de Julio", RoleUser.Administrator, "29/09/2022");
            administrators = new List<Administrator>() { administrator };
        }
        [TestMethod]
        public void InsertAdministratorOk()
        {
            ObgContext context = CreateContext();
            IAdministratorManagement administratorManagement = new AdministratorManagement(context);

            administratorManagement.InsertAdministrator(administrator);

            Administrator administratorInDatabase = context.Administrators.Where<Administrator>(a => a.Name.Equals(administrator.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(administratorInDatabase);
            Assert.AreEqual(administratorInDatabase.Name, administrator.Name);
        }

        [TestMethod]
        public void GetAdministratorsOk()
        {
            IAdministratorManagement administratorManagement = CreateAdministratorManagement();
            IEnumerable<Administrator> administratorsInDatabase = administratorManagement.GetAdministrators();

            Assert.AreEqual(administratorsInDatabase.ToList().Count, administrators.Count);
            Assert.AreEqual(administratorsInDatabase.ToList()[0].Name, administrators[0].Name);

        }

        [TestMethod]
        public void GetAdministratorByNameOk()
        {
            ObgContext context = CreateContext();
            IAdministratorManagement administratorManagement = new AdministratorManagement(context);

            context.Administrators.Add(administrator);
            context.SaveChanges();

            Administrator administratorInDatabase = administratorManagement.GetAdministratorByName(administrator.Name);

            Assert.IsNotNull(administratorInDatabase);
            Assert.AreEqual(administratorInDatabase.Name, administrator.Name);
        }

        [TestMethod]
        public void UpdateAdministratorOk()
        {
            ObgContext context = CreateContext();
            IAdministratorManagement administratorManagement = new AdministratorManagement(context);

            context.Administrators.Add(administrator);
            context.SaveChanges();
            administrator.Address = "25 de Agosto";
            administratorManagement.UpdateAdministrator(administrator);

            Administrator administratorInDatabase = context.Administrators.Where<Administrator>(p => p.Name.Equals(administrator.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(administratorInDatabase);
            Assert.AreEqual(administratorInDatabase.Address, administrator.Address);
        }

        [TestMethod]
        public void DeleteAdministratorOk()
        {
            ObgContext context = CreateContext();
            IAdministratorManagement administratorManagement = new AdministratorManagement(context);

            context.Administrators.Add(administrator);
            context.SaveChanges();

            administratorManagement.DeleteAdministrator(administrator);

            Administrator administratorInDatabase = context.Administrators.Where<Administrator>(p => p.Name.Equals(administrator.Name)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(administratorInDatabase);
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

        private IAdministratorManagement CreateAdministratorManagement()
        {
            var context = CreateContext();

            context.Administrators.Add(administrator);
            context.SaveChanges();

            IAdministratorManagement administratorManagement = new AdministratorManagement(context);
            return administratorManagement;
        }

    }
}
