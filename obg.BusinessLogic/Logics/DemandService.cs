using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class DemandService
    {
        private readonly IDemandManagement _demandManagement;

        public DemandService(IDemandManagement demandManagement)
        {
            _demandManagement = demandManagement;
        }

        public void InsertDemand(Demand demand)
        {
            if (IsDemandValid(demand))
            {
                // Se agrega la Demand a la DB: _demandManagement.InsertDemand(demand);
                FakeDB.Demands.Add(demand);
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
            return true;
        }

        public bool IsIdDemandRegistered(string idDemand)
        {
            foreach (Demand demand in FakeDB.Demands)
            {
                if (demand.IdDemand.Equals(idDemand))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
