﻿using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace obg.Domain.Entities
{
    public class Demand
    {
        [Key] public string IdDemand { get; set; }
        public DemandStatus Status { get; set; }
        public List<Petition> Petitions { get; set; }
        
        public Demand() { }
        public Demand(string idDemand, DemandStatus status)
        {
            IdDemand = idDemand;
            Status = status;
            Petitions = new List<Petition>();
        }

    }
}
