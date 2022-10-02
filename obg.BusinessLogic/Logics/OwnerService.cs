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
    public class OwnerService : UserService
    {
        private readonly IOwnerManagement _ownerManagement;
        private readonly IPharmacyManagement _pharmacyManagement;
        private readonly IInvitationManagement _invitationManagement;

        public OwnerService(IOwnerManagement ownerManagement, IPharmacyManagement pharmacyManagement, IInvitationManagement invitationManagement)
        {
            _ownerManagement = ownerManagement;
            _pharmacyManagement = pharmacyManagement;
            _invitationManagement = invitationManagement;
        }

        public OwnerService() { }

        public Owner InsertOwner(Owner owner)
        {
            Invitation invitation = _invitationManagement.GetInvitationByCode(owner.Code);
            string pharmacyName = invitation.Pharmacy.Name;
            Pharmacy pharmacy = _pharmacyManagement.GetPharmacyByName(pharmacyName);
            //Pharmacy pharmacy = invitation.Pharmacy;
            owner.Pharmacy = pharmacy;

            if (IsUserValid(owner) && HasAPharmacy(owner) && IsAOwner(owner))
            {
                _pharmacyManagement.DeletePharmacy(pharmacy);
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
