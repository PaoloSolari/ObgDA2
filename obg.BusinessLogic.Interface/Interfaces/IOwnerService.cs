using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IOwnerService
    {
        
        //Owner GetOwnerById(int id);
        Owner InsertOwner(Owner owner);
        IEnumerable<User> GetOwners();
        Owner UpdateOwner(Owner owner);
    }
}
