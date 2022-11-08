using obg.Domain.Entities;
using obg.ExporterInterface;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace obg.JSONExporter
{
    public class JSON : IExporter
    {
        public string GetName()
        {
            return "JSON";
        }

        public void ExportData(List<Medicine> medicinesToExport, string path)
        {
            string jsonString = JsonSerializer.Serialize(medicinesToExport);
            File.WriteAllText(path, jsonString);
        }
    }
}
