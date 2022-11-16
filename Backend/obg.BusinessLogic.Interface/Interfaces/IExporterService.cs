
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IExporterService
    {
        string ExportMedicine(string typeOfExporter, string token, Dictionary<string, string> parametersMap);
        public IEnumerable<string> GetExporters();
        List<string> GetParameters(string typeOfExporter);
    }
}
