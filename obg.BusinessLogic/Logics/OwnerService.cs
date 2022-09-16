using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
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
            if (IsUserValid(owner) && HasAPharmacy(owner) && IsAOwner(owner))
            {
                // Se agreaga el Administrator a la DB: _ownerManagement.InsertOwner(owner);
                fakeDB.Add(owner);
            }
            return owner;
        }

        private bool HasAPharmacy(Owner owner)
        {
            if (owner.Pharmacy == null)
            {
                throw new UserException("El dueño no tiene una farmacia asignada.");
            }
            return true; ;
        }

        private bool IsAOwner(Owner owner)
        {
            if (owner.Role != RoleUser.Owner)
            {
                throw new UserException("El dueño tiene asignado un rol incorrecto.");
            }
            return true;
        }

    }
}
