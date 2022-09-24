using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Demand
    {
        public string IdDemand { get; set; }
        public DemandStatus Status { get; set; }
        public List<Petition> Petitions { get; set; }
        public Demand(string idDemand, DemandStatus status, List<Petition> petitions)
        {
            IdDemand = idDemand;
            Status = status;
            Petitions = petitions;
        }
    }
}
