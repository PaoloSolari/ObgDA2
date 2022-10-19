using obg.BusinessLogic.Interface.Interfaces;
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
        private readonly IEmployeeManagement _employeeManagement;

        public DemandService() { }
        public DemandService(IDemandManagement demandManagement, ISessionManagement sessionManagement, IMedicineManagement medicineManagement, IEmployeeManagement employeeManagement)
        {
            _demandManagement = demandManagement;
            _sessionManagement = sessionManagement;
            _medicineManagement = medicineManagement;
            _employeeManagement = employeeManagement;
        }

        public string InsertDemand(Demand demand, string token)
        {
            demand.IdDemand = CreateId();
            demand.Status = DemandStatus.InProgress;
            SetIdsPetitionOfDemand(demand.Petitions);
            if (IsDemandValid(demand))
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                Employee employee = _employeeManagement.GetEmployeeByName(session.UserName);
                Pharmacy employeePharmacy = employee.Pharmacy;
                if (AllMedicinesExistsInPharmacy(employeePharmacy, demand))
                {
                    _demandManagement.InsertDemand(demand, session);
                }
                else
                {
                    throw new DemandException("No existe el medicamento en la farmacia del empleado.");
                }
            }
            return demand.IdDemand;
        }

        private bool AllMedicinesExistsInPharmacy(Pharmacy employeePharmacy, Demand demand)
        {
            foreach (Petition petition in demand.Petitions)
            {
                Medicine medicine = _medicineManagement.GetMedicineByCode(petition.MedicineCode);
                if (!employeePharmacy.Medicines.Contains(medicine))
                {
                    return false;
                }
            }
            return true;
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
                throw new NotFoundException("No existe el medicamento.");
            }
            return true;
        }

        public bool IsIdDemandRegistered(string idDemand)
        {
            return _demandManagement.DemandExists(idDemand);
        }

        private bool ExistAllMedicines(List<Petition> petitions)
        {
            foreach (Petition petition in petitions)
            {
                Medicine medicineCode = _medicineManagement.GetMedicineByCode(petition.MedicineCode);
                if (medicineCode == null)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Demand> GetDemands(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            IEnumerable<Demand> demands = _demandManagement.GetDemands(session);
            if (demands == null)
            {
                throw new NotFoundException("No hay solicitudes de reposición de stock.");
            }
            return demands;
        }

        public string UpdateDemand(string id, Demand demandToUpdate)
        {
            Demand demand = _demandManagement.GetDemandById(id);
            if (demand == null)
            {
                throw new NotFoundException("No existe la solicitud de reposición de stock");
            }
            bool isAlreadySaw = demand.Status == DemandStatus.Accepted || demand.Status == DemandStatus.Rejected;
            if (!isAlreadySaw)
            {
                if ((demandToUpdate.Status == DemandStatus.Accepted) || (demandToUpdate.Status == DemandStatus.Rejected))
                {
                    demand.Status = demandToUpdate.Status;
                    _demandManagement.UpdateDemand(demand);
                }
                else
                {
                    throw new DemandException("Debes aceptar o rechazar la solicitud.");
                }
            }
            else
            {
                throw new DemandException("Ya se realizó la reposición de stock para dicha demanda.");
            }
            return id;
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
