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
        protected List<Demand> fakeDB = new List<Demand>();
        private readonly IDemandManagement _demandManagement;

        public DemandService(IDemandManagement demandManagement)
        {
            _demandManagement = demandManagement;
        }

        public void InsertDemand(Demand demand)
        {
            if (IsDemandValid(demand))
            {
                // Se agreaga la Demand a la DB: _demandManagement.InsertDemand(demand);
                fakeDB.Add(demand);
            }
        }

        private bool IsDemandValid(Demand demand)
        {
            if (demand == null) throw new DemandException("Solicitud inválida.");
            if (demand.Petitions == null || demand.Petitions.Count == 0) throw new DemandException("Solicitud inválida.");
            return true;
        }
    }
}
