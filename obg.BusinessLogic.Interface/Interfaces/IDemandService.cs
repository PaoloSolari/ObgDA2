﻿using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IDemandService
    {
        IEnumerable<Demand> GetDemands();
        Demand InsertDemand(Demand demand);
        Demand UpdateDemand(Demand demand);
    }
}