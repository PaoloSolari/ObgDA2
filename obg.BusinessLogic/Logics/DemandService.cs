﻿using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace obg.BusinessLogic.Logics
{
    public class DemandService : IDemandService
    {
        private readonly IDemandManagement _demandManagement;
        private readonly ISessionManagement _sessionManagement;
        private readonly IMedicineManagement _medicineManagement;

        public DemandService(IDemandManagement demandManagement, ISessionManagement sessionManagement, IMedicineManagement medicineManagement)
        {
            _demandManagement = demandManagement;
            _sessionManagement = sessionManagement;
            _medicineManagement = medicineManagement;
        }

        public DemandService()
        {
        }
        public string InsertDemand(Demand demand, string token)
        {
            demand.IdDemand = CreateId();
            demand.Status = DemandStatus.InProgress;
            SetIdsPetitionOfDemand(demand.Petitions);
            if (IsDemandValid(demand))
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                _demandManagement.InsertDemand(demand, session);
            }
            return demand.IdDemand;
        }

        private string CreateId()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }

        private void SetIdsPetitionOfDemand(List<Petition> petitions)
        {
            foreach (Petition petition in petitions)
            {
                petition.IdPetition = CreateId();
            }
        }

        private bool IsDemandValid(Demand demand)
        {
            if (demand == null)
            {
                throw new DemandException("Solicitud inválida.");
            }
            if (demand.IdDemand == null || demand.IdDemand.Length < 1)
            {
                throw new DemandException("Identificador inválido.");
            }
            if (IsIdDemandRegistered(demand.IdDemand))
            {
                throw new DemandException("Ya existe una solicitud con el mismo identificador");
            }
            if (demand.Petitions == null || demand.Petitions.Count == 0)
            {
                throw new DemandException("Solicitud inválida.");
            }
            if (!ExistAllMedicines(demand.Petitions))
            {
                throw new NotFoundException();
            }
            return true;
        }

        public bool IsIdDemandRegistered(string idDemand)
        {
            return _demandManagement.DemandExists(idDemand);
        }

        private bool ExistAllMedicines(List<Petition> petitions)
        {
            foreach (Petition petition  in petitions)
            {
                Medicine medicineCode = _medicineManagement.GetMedicineByCode(petition.MedicineCode);
                if(medicineCode == null)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Demand> GetDemands()
        {
            IEnumerable<Demand> demands = _demandManagement.GetDemands();
            if(demands == null)
            {
                throw new NotFoundException("No hay solicitudes de reposición de stock.");
            }
            return _demandManagement.GetDemands();
        }

        public string UpdateDemand(Demand demandToUpdate)
        {
            if (IsDemandValid(demandToUpdate)) 
            {
                Demand demand = _demandManagement.GetDemandById(demandToUpdate.IdDemand);
                if (demand == null)
                {
                    throw new NotFoundException("No existe la solicitud de reposición de stock");
                }
                _demandManagement.UpdateDemand(demandToUpdate);
            }
            return demandToUpdate.IdDemand;
        }

        public Demand GetDemandById(string id)
        {
            Demand demand = _demandManagement.GetDemandById(id);
            if (demand == null)
            {
                throw new NotFoundException("La solicitud no existe.");
            }
            return demand;
        }
    }
}
