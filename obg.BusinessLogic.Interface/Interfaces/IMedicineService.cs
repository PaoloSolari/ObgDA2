﻿using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> GetMedicines();
        string InsertMedicine(Medicine medicine, string token);
        Medicine GetMedicineByCode(string code);
        void DeleteMedicine(string code);
        IEnumerable<Medicine> GetMedicinesByName(string medicineName);
    }
}
