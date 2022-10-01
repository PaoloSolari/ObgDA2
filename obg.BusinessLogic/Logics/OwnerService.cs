using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class OwnerService : UserService, IOwnerService
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
                _ownerManagement.InsertOwner(owner);
            }
            return owner;
        }

        private bool HasAPharmacy(Owner owner)
        {
            if (owner.Pharmacy == null)
            {
                throw new UserException("El dueño no tiene una farmacia asignada.");
            }
            return true;
        }

        private bool IsAOwner(Owner owner)
        {
            if (owner.Role != RoleUser.Owner)
            {
                throw new UserException("El dueño tiene asignado un rol incorrecto.");
            }
            return true;
        }

        public IEnumerable<User> GetOwners()
        {
            return _ownerManagement.GetOwners();
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            if (IsUserValid(ownerToUpdate) && HasAPharmacy(ownerToUpdate) && IsAOwner(ownerToUpdate))
            {
                Owner owner = _ownerManagement.GetOwnerByName(ownerToUpdate.Name);
                if (owner == null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }
                _ownerManagement.UpdateOwner(ownerToUpdate);
            }
            return ownerToUpdate;
        }


        public Owner GetOwnerByName(string name)
        {
            return _ownerManagement.GetOwnerByName(name);
        }
    }
}
