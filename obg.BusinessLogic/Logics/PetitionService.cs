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
                // Se agrega la Petition a la DB: _petitionManagement.InsertPetition(petition);
                FakeDB.Petitions.Add(petition);
            }
        }

        private bool IsPetitionValid(Petition petition)
        {
            if (petition == null)
            {
                throw new PetitionException("Petición inválida.");
            }
            if (petition.MedicineCode == null || petition.MedicineCode.Length == 0)
            {
                throw new PetitionException("Código inválido.");
            }
            if (petition.NewQuantity < 1)
            {
                throw new PetitionException("La cantidad no puede ser menor a 1");
            }
            return true;
        }
    }
}
