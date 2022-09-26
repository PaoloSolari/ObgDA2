using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> GetMedicines();
        Medicine GetMedicineById(int id);
        Medicine InsertMedicine(Medicine medicine);
    }
}
