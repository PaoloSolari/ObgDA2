using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class MedicineService : IMedicineService
    {
        protected List<Medicine> fakeDB = new List<Medicine>();
        protected Medicine validMedicine1;
        protected Medicine validMedicine2;

        //private readonly IMedicineManagement _medicineManagement;

        //public MedicineService(IMedicineManagement medicineManagement)
        //{
        //    _medicineManagement = medicineManagement;
        //}

        public MedicineService()
        {
            validMedicine1 = new Medicine("Paracetamol", "aaaa", PresentationMedicine.Capsulas, 0, "1mg", 200, false, true);
            validMedicine2 = new Medicine("Ibuprofeno", "aaaa", PresentationMedicine.Comprimidos, 0, "0.5mg", 100, false, true);

            fakeDB.Add(validMedicine1);
            fakeDB.Add(validMedicine2);

        }

        public Medicine InsertMedicine(Medicine medicine)
        {
            if (IsMedicineValid(medicine))// && !IsCodeRegistered(medicine.Name))
            {
                // Se agreaga la Medicine a la DB: _medicineManagement.InsertMedicine(medicine);
                fakeDB.Add(medicine);
            }
            return medicine;
        }

        public bool IsMedicineValid(Medicine medicine)
        {
            if (medicine == null) throw new MedicineException("Medicamento inválido.");
            //if (medicine.Code == null || medicine.Code.Length < 1) throw new MedicineException("Código inválido.");
            //if (IsCodeRegistered(medicine.Code)) throw new MedicineException("Un medicamento con ese código ya fue registrado");
            if (medicine.Name == null || medicine.Name.Length < 1) throw new MedicineException("Nombre inválido.");
            if (medicine.SymtompsItTreats == null || medicine.SymtompsItTreats.Length < 1) throw new MedicineException("Síntomas a tratar inválidos.");
            if (medicine.Quantity < 0) throw new MedicineException("Cantidad no puede ser menor a cero.");
            if (medicine.Unit == null || medicine.Unit.Length < 1) throw new MedicineException("Unidad inválida.");
            if (medicine.Price < 0) throw new MedicineException("El precio no puede ser menor a cero.");

            return true;
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            return fakeDB;
        }
        public Medicine GetMedicineByCode(string code)
        {

            Medicine auxMedicine = null;
            foreach (Medicine medicine in fakeDB)
            {
                if (medicine.Code.Equals(code))
                {
                    auxMedicine = medicine;
                }
            }
            if (auxMedicine == null)
            {
                throw new MedicineException("El medicamento no existe.");
            }
            return auxMedicine;
        }

        public void DeleteMedicine(string code)
        {
            Medicine medicineToDelete = this.GetMedicineByCode(code);
            if (medicineToDelete == null)
            {
                throw new MedicineException("El medicamento no existe");
            }
            fakeDB.Remove(medicineToDelete);
        }

        public IEnumerable<Medicine> GetMedicineByMedicineName(string medicineName)
        {
            List <Medicine> medicinesFiltered = new List<Medicine>();
            foreach(Medicine medicine in fakeDB)
            {
                if (medicine.Name.Equals(medicineName))
                {
                    medicinesFiltered.Add(medicine);
                }
            }
            if(medicinesFiltered.Count > 0)
            {
                return medicinesFiltered;
            } 
            else
            {
                throw new MedicineException("El medicamento no existe");
            }
        }
    }
}
