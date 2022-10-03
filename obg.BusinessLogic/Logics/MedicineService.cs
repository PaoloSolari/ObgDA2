using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class MedicineService : IMedicineService
    {
        protected List<Medicine> fakeDB = new List<Medicine>();
        protected Medicine validMedicine1;
        protected Medicine validMedicine2;

        private readonly IMedicineManagement _medicineManagement;

        public MedicineService(IMedicineManagement medicineManagement)
        {
            _medicineManagement = medicineManagement;
        }

        public MedicineService()
        {
        }

        public string InsertMedicine(Medicine medicine)
        {
            medicine.IsActive = true;
            medicine.Stock = 0;
            if (IsMedicineValid(medicine))
            {
                _medicineManagement.InsertMedicine(medicine);
            }
            return medicine.Code;
        }

        public bool IsMedicineValid(Medicine medicine)
        {
            if (medicine == null)
            {
                throw new MedicineException("Medicamento inválido.");
            }
            if (medicine.Code == null || medicine.Code.Length < 1)
            {
                throw new MedicineException("Código inválido.");
            }
            if (IsCodeRegistered(medicine.Code))
            {
                throw new MedicineException("Un medicamento con ese código ya fue registrado");
            }
            if (medicine.Name == null || medicine.Name.Length < 1)
            {
                throw new MedicineException("Nombre inválido.");
            }
            if (medicine.SymtompsItTreats == null || medicine.SymtompsItTreats.Length < 1)
            {
                throw new MedicineException("Síntomas a tratar inválidos.");
            }
            if (medicine.Quantity < 0)
            {
                throw new MedicineException("Cantidad no puede ser menor a cero.");
            }
            if (medicine.Unit == null || medicine.Unit.Length < 1)
            {
                throw new MedicineException("Unidad inválida.");
            }
            if (medicine.Price < 0)
            {
                throw new MedicineException("El precio no puede ser menor a cero.");
            }
            return true;
        }

        private bool IsCodeRegistered(string code)
        {
            return _medicineManagement.IsCodeRegistered(code);
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            IEnumerable<Medicine> medicines = _medicineManagement.GetMedicines();
            if (GetLengthOfList(medicines) == 0)
            {
                throw new NotFoundException();
            }
            return _medicineManagement.GetMedicines();
        }

        public int GetLengthOfList(IEnumerable<Medicine> medicines)
        {
            int length = 0;
            foreach(Medicine medicine in medicines)
            {
                length++;
            }
            return length;
        }

        public Medicine GetMedicineByCode(string code)
        {
            return _medicineManagement.GetMedicineByCode(code);
        }

        public void DeleteMedicine(string code)
        {
            Medicine medicine = GetMedicineByCode(code);
            if(medicine == null)
            {
                throw new NotFoundException();
            }
            medicine.IsActive = false;
            //_medicineManagement.DeleteMedicine(medicine);
            _medicineManagement.UpdateMedicine(medicine);
        }

        public IEnumerable<Medicine> GetMedicinesByName(string medicineName)
        {
            IEnumerable<Medicine> medicines = _medicineManagement.GetMedicinesByName(medicineName);
            if(medicines == null)
            {
                throw new NotFoundException();
            }
            return _medicineManagement.GetMedicinesByName(medicineName);
        }
    }
}
