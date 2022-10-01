using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IPharmacyService
    {
        string InsertPharmacy(Pharmacy pharmacy);
    }
}
