using obg.Domain.Entities;
using obg.ExporterInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;
using System.Xml;
using System.Text.Json;
using System.Numerics;

namespace obg.XMLExporter
{
    public class XML : IExporter
    {
        public string GetName()
        {
            return "XML";
        }

        public List<string> ListParameters()
        {
            List<string> parameters = new List<string>();
            parameters.Add("path");
            return parameters;
        }

        public void ExportData(Dictionary<string, string> parametersMap, List<Medicine> medicinesToExport)
        {
            if(parametersMap != null && parametersMap.Count > 0)
            {
                if (parametersMap.ContainsKey("path"))
                {
                    FileInfo file = new FileInfo(parametersMap["path"]);
                    StreamWriter sw = file.AppendText();
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Medicine));
                    foreach (Medicine medicine in medicinesToExport)
                    {
                        writer.Serialize(sw, medicine as Medicine);
                    }
                    sw.Close();
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
