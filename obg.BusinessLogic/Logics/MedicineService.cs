using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class MedicineService
    {
        private readonly IMedicineManagement _medicineManagement;

        public MedicineService(IMedicineManagement medicineManagement)
        {
            _medicineManagement = medicineManagement;
        }

        public void InsertMedicine(Medicine medicine)
        {
            if (IsMedicineValid(medicine))// && !IsCodeRegistered(medicine.Name))
            {
                // Se agreaga la Medicine a la DB: _medicineManagement.InsertMedicine(medicine);
                FakeDB.Medicines.Add(medicine);
            }
        }

        public bool IsMedicineValid(Medicine medicine)
        {
            if (medicine == null)
            {
                throw new MedicineException("Medicamento inválido.");
            }
            //if (medicine.Code == null || medicine.Code.Length < 1)
            //{
            //    throw new MedicineException("Código inválido.");
            //}
            //if (IsCodeRegistered(medicine.Code))
            //{
            //    throw new MedicineException("Un medicamento con ese código ya fue registrado");
            //}
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
    }
}
