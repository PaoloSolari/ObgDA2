using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IDemandManagement
    {
        void InsertDemand(Demand demand, Session session);
        IEnumerable<Demand> GetDemands(Session session);
        Demand GetDemandById(string id);
        void UpdateDemand(Demand demand);
        void DeleteDemand(Demand demand);
        bool DemandExists(string id);
    }
}
