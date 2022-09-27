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
        //private readonly IOwnerManagement _ownerManagement;

        //public OwnerService(IOwnerManagement ownerManagement)
        //{
        //    _ownerManagement = ownerManagement;
        //}

        public Owner InsertOwner(Owner owner)
        {
            if (IsUserValid(owner) && HasAPharmacy(owner) && IsAOwner(owner))
            {
                // Se agrega el Owner a la DB: _ownerManagement.InsertOwner(owner);
                FakeDB.Owners.Add(owner);
                FakeDB.Users.Add(owner);
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
            //return _pharmacyManagement.GetPharmacies();

            return FakeDB.Owners;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            Owner Owner = GetOwnerByName(ownerToUpdate.Name);
            return Owner;
        }


        public Owner GetOwnerByName(string name)
        {

            Owner auxOwner = null;
            foreach (Owner Owner in FakeDB.Owners)
            {
                if (Owner.Name.Equals(name))
                {
                    auxOwner = Owner;
                }
            }
            if (auxOwner == null)
            {
                throw new UserException("El administrador no existe.");
            }
            return auxOwner;
        }
    }
}
