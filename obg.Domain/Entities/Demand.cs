using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Demand
    {
        public int IdDemand { get; set; }
        public DemandStatus Status { get; set; }
        public List<Petition> Petitions { get; set; }
        public Demand(int idDemand, DemandStatus status)
        {
            IdDemand = idDemand;
            Status = status;
            Petitions = new List<Petition>();
        }
    }
}
