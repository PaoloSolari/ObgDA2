using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
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
            ObgContext.Owners.Add(owner);
            ObgContext.SaveChanges();
        }
    }
}
