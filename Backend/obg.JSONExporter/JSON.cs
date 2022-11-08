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

        //  deben "exigirle" a cada exportador que "les diga" qué parámetros necesita.
        // manejarlos con un mapa string -> string (donde la clave es el nombre del parámetro a mostrar en la UI y el valor es el valor real que el usuario termina definiendo).
        // Dictionary<string, string>
        public void ExportData(List<Medicine> medicinesToExport, string path)
        {
            string jsonString = JsonSerializer.Serialize(medicinesToExport);
            File.WriteAllText(path, jsonString);
        }
    }
}
