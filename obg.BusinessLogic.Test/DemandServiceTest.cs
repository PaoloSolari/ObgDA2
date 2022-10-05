using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class DemandServiceTest
    {
        private Mock<IDemandManagement> mock;
        private DemandService service;

        private List<Petition> petitionsFromValidDemand1;
        private List<Petition> petitionsFromValidDemand2;
        private List<Petition> emptyPetitions;
        private List<Petition> nullPetitions;
        private Petition validPetition;
        private Demand validDemand1;
        private Demand validDemand2;
        private Demand nullDemand;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IDemandManagement>(MockBehavior.Strict);
            service = new DemandService(mock.Object);
            
            petitionsFromValidDemand1= new List<Petition>();
            petitionsFromValidDemand2= new List<Petition>();
            emptyPetitions = new List<Petition>();
            nullPetitions = null;
            validPetition = new Petition("XFXCCC", "DDFFFF", 5);

            petitionsFromValidDemand1.Add(validPetition);
            petitionsFromValidDemand2.Add(validPetition);

            validDemand1 = new Demand("AAHHGG", DemandStatus.Accepted);
            validDemand1.Petitions = petitionsFromValidDemand1;
            
            validDemand2 = new Demand("4HIGUF", DemandStatus.InProgress);
            validDemand2.Petitions = petitionsFromValidDemand2;
            nullDemand = null;
        }

        [TestMethod]
        public void InsertDemandOK()
        {
            mock.Setup(x => x.DemandExists(validDemand1.IdDemand)).Returns(false);
            mock.Setup(x => x.InsertDemand(validDemand1));

            service.InsertDemand(validDemand1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_NullDemand()
        {
            service.InsertDemand(nullDemand);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_NullIdDemand()
        {
            validDemand1.IdDemand = null;
            service.InsertDemand(validDemand1);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_EmptyIdDemand()
        {
            validDemand1.IdDemand = "";
            service.InsertDemand(validDemand1);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_RepeatedIdDemand()
        {
            mock.Setup(x => x.DemandExists(validDemand1.IdDemand)).Returns(false);
            mock.Setup(x => x.InsertDemand(validDemand1));

            service.InsertDemand(validDemand1);
            validDemand2.IdDemand = "AAHHGG";

            mock.Setup(x => x.DemandExists(validDemand2.IdDemand)).Returns(true);
            mock.Setup(x => x.InsertDemand(validDemand2));

            service.InsertDemand(validDemand2);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_NullPetitionList()
        {
            mock.Setup(x => x.DemandExists(validDemand1.IdDemand)).Returns(false);
            mock.Setup(x => x.InsertDemand(validDemand1));

            validDemand1.Petitions = nullPetitions;
            service.InsertDemand(validDemand1);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_EmptyPetitionList()
        {
            mock.Setup(x => x.DemandExists(validDemand1.IdDemand)).Returns(false);            mock.Setup(x => x.InsertDemand(validDemand1));
            mock.Setup(x => x.InsertDemand(validDemand1));

            validDemand1.Petitions = emptyPetitions;
            service.InsertDemand(validDemand1);
        }
    }
}
