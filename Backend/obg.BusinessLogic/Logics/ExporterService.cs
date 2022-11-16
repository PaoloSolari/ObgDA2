using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.ExporterInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace obg.BusinessLogic.Logics
{
    public class ExporterService : IExporterService
    {
        private readonly ISessionManagement _sessionManagement;
        private readonly IEmployeeManagement _employeeManagement;
        public ExporterService(ISessionManagement sessionManagement, IEmployeeManagement employeeManagement)
        {
            _sessionManagement = sessionManagement;
            _employeeManagement = employeeManagement;
        }

        public IEnumerable<string> GetExporters()
        {
            List<IExporter> dlls = GetDlls().ToList();
            List<string> dllsNames = new List<string>();
            foreach (var dll in dlls)
            {
                dllsNames.Add(dll.GetName());
            }
            return dllsNames;
        }

        public IEnumerable<IExporter> GetDlls()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Exporters", "*.dll");
            foreach (string file in files)
            {
                Assembly dll = Assembly.UnsafeLoadFrom(file);
                Type type = dll.GetTypes().Where(i => typeof(IExporter).IsAssignableFrom(i)).FirstOrDefault();
                if (type != null)
                {
                    yield return Activator.CreateInstance(type) as IExporter;
                }
            }
        }

        public List<string> GetParameters(string typeOfExporter)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentPath + "\\Exporters", "*.dll");
            List<string> parameters = new List<string>();
            foreach (string file in files)
            {
                Assembly dll = Assembly.LoadFile(file);
                var loadedImplementation = dll.GetTypes().Where(t => typeof(IExporter).IsAssignableFrom(t) && t.IsClass).FirstOrDefault();
                var implementation = Activator.CreateInstance(loadedImplementation) as IExporter;
                if (implementation.GetName().Equals(typeOfExporter))
                {
                    parameters = implementation.ListParameters();
                }
            }
            return parameters;
        }

        public string ExportMedicine(string typeOfExporter, string token, Dictionary<string,string> parametersMap)
        {
            bool implementationLoaded = false;
            string currentPath = Directory.GetCurrentDirectory();
            //string currentPath = @"C:\Users\User\Desktop\DA2 OBG\237912_238855\Backend\obg.WebApi";
            string[] files = Directory.GetFiles(currentPath + "\\Exporters", "*.dll");
            foreach (string file in files)
            {
                Assembly dll = Assembly.LoadFile(file);
                var loadedImplementation = dll.GetTypes().Where(t => typeof(IExporter).IsAssignableFrom(t) && t.IsClass).FirstOrDefault();
                var implementation = Activator.CreateInstance(loadedImplementation) as IExporter;
                if (implementation.GetName().Equals(typeOfExporter))
                {
                    implementationLoaded = true;
                    Employee employee = GetEmployeeBySession(token);
                    Pharmacy employeePharmacy = employee.Pharmacy;
                    //List<Medicine> medicinesToExport = new List<Medicine>();
                    //foreach(string medicineCode in medicinesCodes)
                    //{
                    //    Medicine medicine = _medicineManagement.GetMedicineByCode(medicineCode);
                    //    if (employeePharmacy.Medicines.Contains(medicine))
                    //    {
                    //        medicinesToExport.Add(medicine);
                    //    } 
                    //    else
                    //    {
                    //        throw new ExportException("Solo puedes exportar medicamentos pertenecientes a tu farmacia.");
                    //    }
                    //}
                    if(employeePharmacy.Medicines.Count == 0)
                    {
                        throw new ExportException("La farmacia no tiene medicamentos.");
                    }
                    implementation.ExportData(parametersMap, employeePharmacy.Medicines);
                }
            }
            if (!implementationLoaded)
            {
                throw new ExportException("El formato a exportar no existe.");
            }
            return typeOfExporter;
        }

        public Employee GetEmployeeBySession(string token)
        {
            Session employeeSession = _sessionManagement.GetSessionByToken(token);
            string employeeName = employeeSession.UserName;
            Employee employee = _employeeManagement.GetEmployeeByName(employeeName);
            return employee;
        }
    }
}
