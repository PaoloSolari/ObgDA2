using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class PharmacyManagement : IPharmacyManagement
    {
        private ObgContext ObgContext { get; set; }
        public PharmacyManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertPharmacy(Pharmacy pharmacy)
        {
            ObgContext.Pharmacies.Add(pharmacy);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return ObgContext.Pharmacies.ToList();
        }
    }
}
