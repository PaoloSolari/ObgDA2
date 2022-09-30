using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;

namespace obg.DataAccess.Repositories
{
    public class MedicineManagement : IMedicineManagement
    {
        private ObgContext ObgContext { get; set; }
        public MedicineManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertMedicine(Medicine medicine)
        {
            ObgContext.Medicines.Add(medicine);
            ObgContext.SaveChanges();
        }
    }
}