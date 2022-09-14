using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class OwnerService : UserService
    {
        private readonly IOwnerManagement _ownerManagement;

        public OwnerService(IOwnerManagement ownerManagement)
        {
            _ownerManagement = ownerManagement;
        }

        public Owner InsertOwner(Owner owner)
        {
            if (IsUserValid(owner) && owner.Pharmacy != null)
            {
                // Se agreaga el Administrator a la DB.
                fakeDB.Add(owner);
            }
            return owner;
        }
    }
}
