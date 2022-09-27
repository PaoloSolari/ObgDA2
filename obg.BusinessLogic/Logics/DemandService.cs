﻿using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class DemandService : IDemandService
    {
        protected List<Demand> fakeDB = new List<Demand>();
        Demand validDemand;
        Demand validDemand2;
        private Petition validPetition1;

        //private readonly IDemandManagement _demandManagement;

        //public DemandService(IDemandManagement demandManagement)
        //{
        //    _demandManagement = demandManagement;
        //}

        public DemandService()
        {
            validDemand = new Demand(2, DemandStatus.InProgress);
            validDemand2 = new Demand(1, DemandStatus.Rejected);
            validPetition1 = new Petition("aaaaa", 5);
            validDemand.Petitions.Add(validPetition1);
            validDemand2.Petitions.Add(validPetition1);
            fakeDB.Add(validDemand);
            fakeDB.Add(validDemand2);
        }
        public Demand InsertDemand(Demand demand)
        {
            if (IsDemandValid(demand))
            {
                // Se agreaga la Demand a la DB: _demandManagement.InsertDemand(demand);
                fakeDB.Add(demand);
            }
            return demand;
        }

        private bool IsDemandValid(Demand demand)
        {
            if (demand == null) throw new DemandException("Solicitud inválida.");
            if (demand.Petitions == null || demand.Petitions.Count == 0) throw new DemandException("Solicitud inválida.");
            return true;
        }

        public IEnumerable<Demand> GetDemands()
        {
            List<Demand> demandsInProgress = new List<Demand>();
            foreach(Demand demand in fakeDB)
            {
                if(demand.Status.Equals(DemandStatus.InProgress))
                {
                    demandsInProgress.Add(demand);
                }
            }
            if(demandsInProgress.Count == 0)
            {
                throw new DemandException("No hay solicitudes disponibles");
            }
            return demandsInProgress;
        }

        public Demand UpdateDemand(Demand demandToUpdate)
        {
            Demand demand = this.GetDemandById(demandToUpdate.IdDemand);
            return demand;
        }

        public Demand GetDemandById(int id)
        {

            Demand auxDemand = null;
            foreach (Demand demand in fakeDB)
            {
                if (demand.IdDemand == id)
                {
                    auxDemand = demand;
                }
            }
            if (auxDemand == null)
            {
                throw new DemandException("La solicitud no existe.");
            }
            return auxDemand;
        }
    }
}
