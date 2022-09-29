using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
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
            ObgContext.Administrators.Add(administrator);
            ObgContext.SaveChanges();
        }
    }
}
