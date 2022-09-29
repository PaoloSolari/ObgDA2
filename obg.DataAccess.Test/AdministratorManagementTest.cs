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
            administrator = new Administrator("Gabriel", 987654, "gabriel@gmail.com", "ahi1244..dd", "Avenida 33", RoleUser.Administrator, "29/09/2022");
            administrators = new List<Administrator>() { administrator };
        }
        [TestMethod]
        public void InsertAdministratorOk()
        {
            ObgContext context = CreateContext();
            IAdministratorManagement administratorManagement = new AdministratorManagement(context);

            administratorManagement.InsertAdministrator(administrator);

            Administrator administratorInDatabase = context.Administrators.Where<Administrator>(a => a.Name == administrator.Name).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(administratorInDatabase);
            Assert.AreEqual(administratorInDatabase.Name, administrator.Name);
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
