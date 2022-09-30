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
    public class PetitionManagementTest
    {
        private Petition petition;
        private List<Petition> petitions;

        [TestInitialize]
        public void InitTest()
        {
            petition = new Petition("SSWWEE", "FER5TR", 30);
            
            petitions = new List<Petition>() { petition };
        }

        [TestMethod]
        public void InsertPetitionOk()
        {
            ObgContext context = CreateContext();
            IPetitionManagement petitionManagement = new PetitionManagement(context);

            petitionManagement.InsertPetition(petition);

            Petition petitionInDatabase = context.Petitions.Where<Petition>(p => p.IdPetition == petition.IdPetition).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(petitionInDatabase);
            Assert.AreEqual(petitionInDatabase.IdPetition, petition.IdPetition);
        }

        [TestMethod]
        public void GetPetitionsOk()
        {
            IPetitionManagement petitionManagement = CreatePetitionManagement();
            IEnumerable<Petition> petitionsInDatabase = petitionManagement.GetPetitions();

            Assert.AreEqual(petitionsInDatabase.ToList().Count, petitions.Count);
            Assert.AreEqual(petitionsInDatabase.ToList()[0].IdPetition, petitions[0].IdPetition);

        }

        [TestMethod]
        public void GetPetitionByIdOk()
        {
            ObgContext context = CreateContext();
            IPetitionManagement petitionManagement = new PetitionManagement(context);

            context.Petitions.Add(petition);
            context.SaveChanges();

            Petition petitionInDatabase = petitionManagement.GetPetitionById(petition.IdPetition);

            Assert.IsNotNull(petitionInDatabase);
            Assert.AreEqual(petitionInDatabase.IdPetition, petition.IdPetition);
        }

        [TestMethod]
        public void UpdatePetitionOk()
        {
            ObgContext context = CreateContext();
            IPetitionManagement petitionManagement = new PetitionManagement(context);

            context.Petitions.Add(petition);
            context.SaveChanges();
            petition.NewQuantity = 50;
            petitionManagement.UpdatePetition(petition);

            Petition petitionInDatabase = context.Petitions.Where<Petition>(p => p.IdPetition == petition.IdPetition).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(petitionInDatabase);
            Assert.AreEqual(petitionInDatabase.NewQuantity, petition.NewQuantity);
        }

        [TestMethod]
        public void DeletePetitionOk()
        {
            ObgContext context = CreateContext();
            IPetitionManagement petitionManagement = new PetitionManagement(context);

            context.Petitions.Add(petition);
            context.SaveChanges();

            petitionManagement.DeletePetition(petition);

            Petition petitionInDatabase = context.Petitions.Where<Petition>(p => p.IdPetition == petition.IdPetition).AsNoTracking().FirstOrDefault();

            Assert.IsNull(petitionInDatabase);
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

        private IPetitionManagement CreatePetitionManagement()
        {
            var context = CreateContext();

            context.Petitions.Add(petition);
            context.SaveChanges();

            IPetitionManagement petitionManagement = new PetitionManagement(context);
            return petitionManagement;
        }
    }
}
