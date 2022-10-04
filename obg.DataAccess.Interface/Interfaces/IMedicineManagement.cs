using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IMedicineManagement
    {
        void InsertMedicine(Medicine medicine, Session session);
        IEnumerable<Medicine> GetMedicines();
        Medicine GetMedicineByCode(string code);
        void UpdateMedicine(Medicine medicine);
        void DeleteMedicine(Medicine medicine);
        bool IsCodeRegistered(string code);
        IEnumerable<Medicine> GetMedicinesByName(string name);
    }
}
