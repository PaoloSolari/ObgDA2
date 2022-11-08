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

        public void ExportData(List<Medicine> medicinesToExport, string path)
        {
            FileInfo file = new FileInfo(path);
            StreamWriter sw = file.AppendText();
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Medicine));
            foreach (Medicine medicine in medicinesToExport)
            {
                writer.Serialize(sw, medicine as Medicine);
            }
            sw.Close();
        }
    }
}
