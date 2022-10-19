using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IDemandService
    {
        IEnumerable<Demand> GetDemands(string token);
        string InsertDemand(Demand demand, string token);
        string UpdateDemand(string id, Demand demand);
    }
}
