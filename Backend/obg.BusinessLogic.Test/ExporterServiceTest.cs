using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace obg.BusinessLogic.Test
{
    [TestClass]
    public class ExporterServiceTest
    {
        private Mock<ISessionManagement> mockSession;
        private Mock<IEmployeeManagement> mockEmployee;
        private ExporterService service;

        private Medicine medicine;
        private Session validSession1;
        private Employee validEmployee;
        private Pharmacy validPharmacy1;
        private List<string> medicineCodes;
        private Dictionary<string, string> parametersMap;

        [TestInitialize]
        public void InitTest()
        {
            mockSession = new Mock<ISessionManagement>(MockBehavior.Strict);
            mockEmployee = new Mock<IEmployeeManagement>(MockBehavior.Strict);

            service = new ExporterService(mockSession.Object, mockEmployee.Object);

            medicine = new Medicine("DDFFFF", "Paracetamol", "Dolor de cabeza", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);

            validPharmacy1 = new Pharmacy("FarmaUy", "Gaboto");
            validPharmacy1.Medicines.Add(medicine);
            validEmployee = new Employee("Rodrigo", 000101, "r@gmail.com", "$$$aaa123.", "addressR", RoleUser.Employee, "13/09/2022", validPharmacy1);
            validSession1 = new Session("DDIQDS", validEmployee.Name, "4de12a");

            medicineCodes = new List<string>();
            medicineCodes.Add(medicine.Code);

            parametersMap = new Dictionary<string, string>();
            parametersMap.Add("path", "aaa");
        }

        [TestMethod]
        public void ExportMedicineOK()
        {
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            service.ExportMedicine("JSON", validSession1.Token, parametersMap);

            mockSession.VerifyAll();
            mockEmployee.VerifyAll();
        }

        [ExpectedException(typeof(ExportException))]
        [TestMethod]
        public void ExportMedicineWrong_NullMedicineCodes()
        {
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            service.ExportMedicine("JSON", validSession1.Token, parametersMap);
        }        
        
        [ExpectedException(typeof(System.Exception))]
        [TestMethod]
        public void ExportMedicineWrong_NullParametersMap()
        {
            mockSession.Setup(x => x.GetSessionByToken(validSession1.Token)).Returns(validSession1);
            mockEmployee.Setup(x => x.GetEmployeeByName(validSession1.UserName)).Returns(validEmployee);
            service.ExportMedicine("JSON", validSession1.Token, null);
        }
    }
}
