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

        public DemandService(IDemandManagement demandManagement)
        {
            _demandManagement = demandManagement;
        }

        public DemandService()
        {
        }
        public string InsertDemand(Demand demand)
        {
            if (IsDemandValid(demand))
            {
                _demandManagement.InsertDemand(demand);
            }
            return demand.IdDemand;
        }

        public bool IsDemandValid(Demand demand)
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
            return true;
        }

        public bool IsIdDemandRegistered(string idDemand)
        {
            return _demandManagement.DemandExists(idDemand);
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
