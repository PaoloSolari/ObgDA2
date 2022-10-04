using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PetitionService
    {
        private readonly IPetitionManagement _petitionManagement;

        public PetitionService(IPetitionManagement petitionManagement)
        {
            _petitionManagement = petitionManagement;
        }

        public void InsertPetition(Petition petition)
        {
            if (IsPetitionValid(petition))
            {
                _petitionManagement.InsertPetition(petition);
            }
        }

        private bool IsPetitionValid(Petition petition)
        {
            if (petition == null)
            {
                throw new PetitionException("Petición inválida.");
            }
            if (petition.IdPetition == null || petition.IdPetition.Length < 1)
            {
                throw new PetitionException("Identificador inválido.");
            }
            if (IsIdPetitionRegistered(petition.IdPetition))
            {
                throw new PetitionException("Ya existe una petitición con el mismo identificador");
            }
            if (petition.MedicineCode == null || petition.MedicineCode.Length == 0)
            {
                throw new PetitionException("Código inválido.");
            }
            if (!IsMedicineCodeOk(petition.MedicineCode))
            {
                throw new PetitionException("El medicamento no existe.");
            }
            if (petition.NewQuantity < 1)
            {
                throw new PetitionException("La cantidad no puede ser menor a 1");
            }
            return true;
        }

        public bool IsIdPetitionRegistered(string idPetition)
        {
            return _petitionManagement.IsIdPetitionRegistered(idPetition);
        }

        private bool IsMedicineCodeOk(string medicineCode)
        {
            // Se chequea que la medicina de la petición exista en la DB.
            return _petitionManagement.IsMedicineCodeOk(medicineCode);
        }
    }
}
