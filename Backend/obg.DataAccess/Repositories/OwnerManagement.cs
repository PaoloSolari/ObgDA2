﻿using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class OwnerManagement : IOwnerManagement
    {
        private ObgContext ObgContext { get; set; }
        public OwnerManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertOwner(Owner owner)
        {
            string pharmacyName = owner.Pharmacy.Name;
            Pharmacy pharmacyOfOwner = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(pharmacyName)).Include("Medicines").Include("Demands.Petitions").FirstOrDefault();
            if (pharmacyOfOwner != null)
            {
                ObgContext.Attach(owner.Pharmacy);
            }
            ObgContext.Owners.Add(owner);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Owner> GetOwners()
        {
            return ObgContext.Owners.ToList();
        }

        public Owner GetOwnerByName(string name)
        {
            return ObgContext.Owners.Where<Owner>(o => o.Name == name).FirstOrDefault();
        }

        public void UpdateOwner(Owner owner)
        {
            ObgContext.Owners.Attach(owner);
            ObgContext.Entry(owner).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteOwner(Owner owner)
        {
            ObgContext.Owners.Remove(owner);
            ObgContext.SaveChanges();
        }

    }
}
