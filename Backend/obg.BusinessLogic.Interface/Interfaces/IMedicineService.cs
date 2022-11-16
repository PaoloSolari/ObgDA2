using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> GetAllMedicines();
        IEnumerable<Medicine> GetMedicines(string employeeName);
        string InsertMedicine(Medicine medicine, string token);
        Medicine GetMedicineByCode(string code);
        IEnumerable<Medicine> GetMedicinesWithStock(string medicineName);
        void DeleteMedicine(string code);
        IEnumerable<Medicine> GetMedicinesByName(string medicineName);
    }
}
