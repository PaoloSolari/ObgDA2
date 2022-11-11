using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IPharmacyService
    {
        IEnumerable<Pharmacy> GetPharmacies();
        Pharmacy GetPharmacyByName(string Name);
        string InsertPharmacy(Pharmacy pharmacy, string token);
    }
}
