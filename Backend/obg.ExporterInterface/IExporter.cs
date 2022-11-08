
using obg.Domain.Entities;
using System;
using System.Collections.Generic;

namespace obg.ExporterInterface
{
    public interface IExporter
    {
        public string GetName();
        public void ExportData(List<Medicine> medicinesToExport, string path);
    }
}
