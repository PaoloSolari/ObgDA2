using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationManagement _invitationManagement;
        private readonly IPharmacyManagement _pharmacyManagement;

        public InvitationService(IInvitationManagement invitationManagement, IPharmacyManagement pharmacyManagement)
        {
            _invitationManagement = invitationManagement;
            _pharmacyManagement = pharmacyManagement;
        }

        public int InsertInvitation(Invitation invitation, string pharmacyName)
        {
            Pharmacy pharmacy = _pharmacyManagement.GetPharmacyByName(pharmacyName);
            bool noAdministrator = invitation.UserRole != 0;
            if (noAdministrator && pharmacy == null)
            {
                throw new NotFoundException("No existe la farmacia.");
            }

            invitation.IdInvitation = CreateId();
            invitation.Pharmacy = pharmacy;
            invitation.UserCode = CreateCode();

            if (IsInvitationValid(invitation))
            {
                _invitationManagement.InsertInvitation(invitation);
            }
            return invitation.UserCode;
        }

        private string CreateId()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }

        private int CreateCode()
        {
            Random random = new Random();
            string ramdomString = random.Next(0, 1000000).ToString("D6");
            int randomInt = Int32.Parse(ramdomString);
            return randomInt;
        }

        private bool IsInvitationValid(Invitation invitation)
        {
            if (invitation == null)
            {
                throw new InvitationException("Invitación inválida.");
            }
            if (invitation.IdInvitation == null || invitation.IdInvitation.Length < 1)
            {
                throw new InvitationException("Identificador inválido.");
            }
            if (IsIdInvitationRegistered(invitation.IdInvitation))
            {
                throw new InvitationException("Ya existe una invitación con el mismo identificador");
            }
            if (invitation.UserRole != 0 && invitation.Pharmacy == null)
            {
                throw new InvitationException("Farmacia inválida.");
            }
            if (invitation.UserName == null || invitation.UserName.Length == 0 || invitation.UserName.Length > 20)
            {
                throw new InvitationException("Nombre de usuario inválido.");
            }
            if (IsNameRegistered(invitation.UserName))
            {
                throw new InvitationException("El nombre ya fue registrado.");
            }
            if (invitation.UserCode.ToString("D6").Length != 6)
            {
                throw new InvitationException("Código de usuario inválido.");
            }
            if (IsCodeRegistered(invitation.UserCode))
            {
                throw new InvitationException("El nombre ya fue registrado.");
            }
            return true;
        }

        public bool IsIdInvitationRegistered(string idInvitation)
        {
            return _invitationManagement.IsIdInvitationRegistered(idInvitation);
        }

        public bool IsNameRegistered(string name)
        {
            return _invitationManagement.IsNameRegistered(name);
        }

        public bool IsCodeRegistered(int code) 
        {
            return _invitationManagement.IsCodeRegistered(code);
        }

    }
}
