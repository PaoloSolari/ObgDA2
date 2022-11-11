
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IExporterService
    {
        List<string> ExportMedicine(List<string> medicinesCodes, string typeOfExporter, string token, Dictionary<string, string> parametersMap);
        public IEnumerable<string> GetExporters();
    }
}
