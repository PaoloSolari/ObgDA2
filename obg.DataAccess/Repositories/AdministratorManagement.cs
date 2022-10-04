using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class AdministratorManagement : IAdministratorManagement
    {
        private ObgContext ObgContext { get; set; }
        public AdministratorManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertAdministrator(Administrator administrator)
        {
            //IEnumerable<Pharmacy> pharmacies = ObgContext.Pharmacies.ToList();
            //administrator.Pharmacies = new List<Pharmacy>();
            //foreach (Pharmacy pharmacy in pharmacies)
            //{
            //    administrator.Pharmacies.Add(pharmacy);
            //}
            //foreach (Pharmacy pharmacy in pharmacies)
            //{
            //    //administrator.Pharmacies.Add(pharmacy);
            //    ObgContext.Attach(pharmacy);
            //}
            ObgContext.Administrators.Add(administrator);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Administrator> GetAdministrators()
        {
            return ObgContext.Administrators.ToList();
        }

        public Administrator GetAdministratorByName(string name)
        {
            return ObgContext.Administrators.Where<Administrator>(a => a.Name == name).AsNoTracking().FirstOrDefault();
        }

        public void UpdateAdministrator(Administrator administrator)
        {
            ObgContext.Administrators.Attach(administrator);
            ObgContext.Entry(administrator).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteAdministrator(Administrator administrator)
        {
            ObgContext.Administrators.Remove(administrator);
            ObgContext.SaveChanges();
        }

    }
}
