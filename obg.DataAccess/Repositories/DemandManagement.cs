using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;

namespace obg.DataAccess.Repositories
{
    public class DemandManagement : IDemandManagement
    {
        private ObgContext ObgContext { get; set; }
        public DemandManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertDemand(Demand demand)
        {
            ObgContext.Demands.Add(demand);
            ObgContext.SaveChanges();
        }
    }
}