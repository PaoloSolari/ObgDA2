using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IOwnerManagement
    {
        void InsertOwner(Owner owner);
        IEnumerable<Owner> GetOwners();
        Owner GetOwnerByName(string name);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }
}
