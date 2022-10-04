using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IPharmacyManagement
    {
        void InsertPharmacy(Pharmacy pharmacy, Session session);
        IEnumerable<Pharmacy> GetPharmacies();
        Pharmacy GetPharmacyByName(string name);
        void UpdatePharmacy(Pharmacy pharmacy);
        void DeletePharmacy(Pharmacy pharmacy);
        bool IsNameRegistered(string name);
    }
}
