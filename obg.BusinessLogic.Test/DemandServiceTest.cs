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

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class DemandServiceTest
    {
        private Mock<IDemandManagement> mock;
        private DemandService service;
        private Demand validDemand1;
        private Demand validDemand2;
        private Demand nullDemand;
        private Petition validPetition1;
        private List<Petition> nullPetitionList;
        private List<Petition> emptyPetitionList;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IDemandManagement>(MockBehavior.Strict);
            service = new DemandService(mock.Object);
            validPetition1 = new Petition(1, "aaaaa", 5);
            nullPetitionList = null;
            emptyPetitionList = new List<Petition>();
            validDemand1 = new Demand(1, DemandStatus.Accepted);
            validDemand2 = new Demand(2, DemandStatus.InProgress);
            validDemand1.Petitions.Add(validPetition1);
            validDemand2.Petitions.Add(validPetition1);
            nullDemand = null;
        }

        [TestMethod]
        public void InsertDemandOK()
        {
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
        public void InsertDemandWrong_NullPetitionList()
        {
            validDemand1.Petitions = nullPetitionList;
            service.InsertDemand(validDemand1);
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_EmptyPetitionList()
        {
            validDemand1.Petitions = emptyPetitionList;
            service.InsertDemand(validDemand1);
        }
    }
}
