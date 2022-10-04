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
        protected Medicine validMedicine1;
        protected Medicine validMedicine2;

        private readonly IMedicineManagement _medicineManagement;
        private readonly ISessionManagement _sessionManagement;

        public MedicineService(IMedicineManagement medicineManagement, ISessionManagement sessionManagement)
        {
            _medicineManagement = medicineManagement;
            _sessionManagement = sessionManagement;
        }

        public MedicineService()
        {
        }

        public string InsertMedicine(Medicine medicine, string token)
        {
            medicine.IsActive = true;
            medicine.Stock = 0;
            if (IsMedicineValid(medicine))
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                _medicineManagement.InsertMedicine(medicine, session);
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
            IEnumerable<Medicine> medicinesFromDB = _medicineManagement.GetMedicines();
            foreach (Medicine medicine in medicinesFromDB)
            {
                if (medicine.IsActive && medicine.Code.Equals(code))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            // Corroborar que sean los medicamentos de la farmacia del empleado.
            IEnumerable<Medicine> medicines = _medicineManagement.GetMedicines();
            if (GetLengthOfList(medicines) == 0)
            {
                throw new NotFoundException();
            }
            IEnumerable<Medicine> ActivesMedicines = GetActivesMedicines(medicines);
            if (GetLengthOfList(ActivesMedicines) == 0)
            {
                throw new NotFoundException();
            }
            return ActivesMedicines;
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

        public IEnumerable<Medicine> GetActivesMedicines(IEnumerable<Medicine> medicines)
        {
            List<Medicine> activesMedicines = new List<Medicine>();
            foreach(Medicine medicine in medicines)
            {
                if (medicine.IsActive)
                {
                    activesMedicines.Add(medicine);
                }
            }
            return activesMedicines;
        }

        public Medicine GetMedicineByCode(string code)
        {
            return _medicineManagement.GetMedicineByCode(code);
        }

        public void DeleteMedicine(string code)
        {
            Medicine medicine = GetMedicineByCode(code);
            if (medicine == null)
            {
                throw new NotFoundException();
            }
            _medicineManagement.DeleteMedicine(medicine);
        }

        public IEnumerable<Medicine> GetMedicinesByName(string medicineName)
        {
            IEnumerable<Medicine> medicines = _medicineManagement.GetMedicinesByName(medicineName);
            if(medicines == null)
            {
                throw new NotFoundException();
            }
            return medicines;
        }

        public IEnumerable<Medicine> GetMedicinesWithStock(string medicineName)
        {
            List<Medicine> allMedicines = _medicineManagement.GetMedicinesByName(medicineName).ToList();
            if (allMedicines == null)
            {
                throw new NotFoundException();
            }
            List<Medicine> medicinesWithStock = new List<Medicine>();
            foreach (Medicine medicine in allMedicines)
            {
                if(medicine.Stock > 0)
                {
                    medicinesWithStock.Add(medicine);
                }
            }
            if(medicinesWithStock.Count < 1)
            {
                throw new MedicineException("No hay stock disponible del medicamento en el sistema.");
            }
            return medicinesWithStock;
        }

    }
}
