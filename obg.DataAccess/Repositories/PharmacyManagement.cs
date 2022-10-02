using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
            //Owner ownerOfPharmacy = ObgContext.Owners.Where<Owner>(o => o.Pharmacy == pharmacy).AsNoTracking().FirstOrDefault();
            //if (ownerOfPharmacy != null)
            //{
            //    //pharmacy.Owner = ownerOfPharmacy;
            //    pharmacy.Owner = null;
            //}
            ObgContext.Pharmacies.Add(pharmacy);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return ObgContext.Pharmacies.ToList();
        }

        public Pharmacy GetPharmacyByName(string name)
        {
            return ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name == name).AsNoTracking().FirstOrDefault();
        }

        public void UpdatePharmacy(Pharmacy pharmacy)
        {
            ObgContext.Pharmacies.Attach(pharmacy);
            ObgContext.Entry(pharmacy).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeletePharmacy(Pharmacy pharmacy)
        {
            Invitation invitationDependPharmacy = ObgContext.Invitations.Where<Invitation>(i => i.Pharmacy == pharmacy).AsNoTracking().FirstOrDefault();
            if (invitationDependPharmacy != null)
            {
                ObgContext.Invitations.Remove(invitationDependPharmacy);
            }
            ObgContext.Pharmacies.Remove(pharmacy);
            
            ObgContext.SaveChanges();
        }

        public bool IsNameRegistered(string name)
        {
            Pharmacy pharmacy = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name == name).AsNoTracking().FirstOrDefault();
            if(pharmacy != null)
            {
                return true;
            }
            return false;
        }
    }
}
