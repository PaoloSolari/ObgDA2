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
        private Mock<ISessionManagement> mockSession;
        private Mock<IMedicineManagement> mockMedicine;
        private Mock<IEmployeeManagement> mockEmployee;
        private DemandService service;

        private List<Petition> petitionsFromValidDemand1;
        private List<Petition> petitionsFromValidDemand2;
        private List<Petition> emptyPetitions;
        private List<Petition> nullPetitions;
        private Medicine medicine;
        private Petition validPetition;
        private Demand validDemand1;
        private Demand validDemand2;
        private Demand nullDemand;
        private Session validSession1;
        private Employee validEmployee;
        private Pharmacy validPharmacy1;



        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IDemandManagement>(MockBehavior.Strict);
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            mockMedicine = new Mock<IMedicineManagement>(MockBehavior.Strict);
            mockEmployee = new Mock<IEmployeeManagement>(MockBehavior.Strict);

            service = new DemandService(mock.Object, mockSession.Object, mockMedicine.Object, mockEmployee.Object);

            petitionsFromValidDemand1 = new List<Petition>();
            petitionsFromValidDemand2 = new List<Petition>();
            emptyPetitions = new List<Petition>();
            nullPetitions = null;
            medicine = new Medicine("DDFFFF", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            validPetition = new Petition("XFXCCC", "DDFFFF", 5);

            petitionsFromValidDemand1.Add(validPetition);
            petitionsFromValidDemand2.Add(validPetition);

            validDemand1 = new Demand("AAHHGG", DemandStatus.Accepted);
            validDemand1.Petitions = petitionsFromValidDemand1;

            validDemand2 = new Demand("4HIGUF", DemandStatus.InProgress);
            validDemand2.Petitions = petitionsFromValidDemand2;
            nullDemand = null;

            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validPharmacy1.Medicines.Add(medicine);
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validSession1 = new Session("DDIQDS", validEmployee.Name, "4de12a");

        }

        [TestMethod]
        public void InsertDemandOK()
        {
            mock.Setup(x => x.DemandExists(It.IsAny<string>())).Returns(false);
            mockMedicine.Setup(x => x.GetMedicineByCode(validDemand1.Petitions[0].MedicineCode)).Returns(medicine);
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            mock.Setup(x => x.InsertDemand(validDemand1, validSession1));
            service.InsertDemand(validDemand1, validSession1.Token);

            mock.VerifyAll();
            mockMedicine.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertDemandWrong_NullDemand()
        {
            service.InsertDemand(nullDemand, validSession1.Token);
        }


        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_RepeatedIdDemand()
        {
            mock.Setup(x => x.DemandExists(It.IsAny<string>())).Returns(false);
            mockMedicine.Setup(x => x.GetMedicineByCode(validDemand1.Petitions[0].MedicineCode)).Returns(medicine);
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            mock.Setup(x => x.InsertDemand(validDemand1, validSession1));
            service.InsertDemand(validDemand1, validSession1.Token);

            mock.VerifyAll();
            mockMedicine.VerifyAll();
            mockSession.VerifyAll();

            validDemand2.IdDemand = "AAHHGG";

            mock.Setup(x => x.DemandExists(It.IsAny<string>())).Returns(true);
            mockMedicine.Setup(x => x.GetMedicineByCode(validDemand1.Petitions[0].MedicineCode)).Returns(medicine);
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            mock.Setup(x => x.InsertDemand(validDemand1, validSession1));
            service.InsertDemand(validDemand1, validSession1.Token);

            mock.VerifyAll();
            mockMedicine.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertDemandWrong_NullPetitionList()
        {
            mock.Setup(x => x.DemandExists(It.IsAny<string>())).Returns(false);
            mockMedicine.Setup(x => x.GetMedicineByCode(validDemand1.Petitions[0].MedicineCode)).Returns(medicine);
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            mock.Setup(x => x.InsertDemand(validDemand1, validSession1));


            validDemand1.Petitions = nullPetitions;
            service.InsertDemand(validDemand1, validSession1.Token);

            mock.VerifyAll();
            mockMedicine.VerifyAll();
            mockSession.VerifyAll();
        }

        [ExpectedException(typeof(DemandException))]
        [TestMethod]
        public void InsertDemandWrong_EmptyPetitionList()
        {
            mock.Setup(x => x.DemandExists(It.IsAny<string>())).Returns(false);
            mockMedicine.Setup(x => x.GetMedicineByCode(validDemand1.Petitions[0].MedicineCode)).Returns(medicine);
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            mock.Setup(x => x.InsertDemand(validDemand1, validSession1));


            validDemand1.Petitions =  new List<Petition>();
            service.InsertDemand(validDemand1, validSession1.Token);
            validDemand1.Petitions = emptyPetitions;
            mock.VerifyAll();
            mockMedicine.VerifyAll();
            mockSession.VerifyAll();
        }
    }
}
