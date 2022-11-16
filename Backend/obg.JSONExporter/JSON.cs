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

        public List<string> ListParameters()
        {
            List<string> parameters = new List<string>();
            parameters.Add("path");
            return parameters;
        }     
        public void ExportData(Dictionary<string, string> parametersMap, List<Medicine> medicinesToExport)
        {
            if (parametersMap != null && parametersMap.Count > 0)
            {
                if (parametersMap.ContainsKey("path"))
                {
                    string jsonString = JsonSerializer.Serialize(medicinesToExport);
                    File.WriteAllText(parametersMap["path"], jsonString);
                } 
                else
                {
                    throw new System.Exception("Debes especificar el path donde quieres exportar el archivo");
                }
            }
            else
            {
                throw new System.Exception("Faltan datos para poder exportar el archivo.");
            }
        }
    }
}
