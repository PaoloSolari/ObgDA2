﻿using Microsoft.EntityFrameworkCore;
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

        public void InsertPharmacy(Pharmacy pharmacy, Session session)
        {
            string admnistratorName = session.UserName;
            Administrator admnistratorOfPharmacy = ObgContext.Administrators.Where<Administrator>(a => a.Name.Equals(admnistratorName)).Include("Pharmacies").FirstOrDefault();
            if(admnistratorOfPharmacy != null)
            {
                if(admnistratorOfPharmacy.Pharmacies != null)
                {
                    admnistratorOfPharmacy.Pharmacies.Add(pharmacy);
                }
                else
                {
                    admnistratorOfPharmacy.Pharmacies = new List<Pharmacy>();
                    admnistratorOfPharmacy.Pharmacies.Add(pharmacy);
                }
                ObgContext.Attach(admnistratorOfPharmacy);
            }
            
            ObgContext.Pharmacies.Add(pharmacy);
            ObgContext.SaveChanges();
        }


        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return ObgContext.Pharmacies.Include("Medicines").Include("Purchases");
        }

        public Pharmacy GetPharmacyByName(string name)
        {
            return ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name == name).Include("Medicines").Include("Purchases").FirstOrDefault();
        }

        public void UpdatePharmacy(Pharmacy pharmacy)
        {
            ObgContext.Pharmacies.Attach(pharmacy);
            ObgContext.Entry(pharmacy).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeletePharmacy(Pharmacy pharmacy)
        {
            Invitation invitationDependPharmacy = ObgContext.Invitations.Where<Invitation>(i => i.Pharmacy == pharmacy).FirstOrDefault();
            if (invitationDependPharmacy != null)
            {
                ObgContext.Invitations.Remove(invitationDependPharmacy);
            }
            ObgContext.Pharmacies.Remove(pharmacy);
            
            ObgContext.SaveChanges();
        }

        public bool IsNameRegistered(string name)
        {
            Pharmacy pharmacy = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name == name).FirstOrDefault();
            if(pharmacy != null)
            {
                return true;
            }
            return false;
        }
    }
}
