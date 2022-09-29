using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class InvitationService : IInvitationService
    {
        protected List<Invitation> fakeDB = new List<Invitation>();
        private readonly IInvitationManagement _invitationManagement;

        public InvitationService(IInvitationManagement invitationManagement)
        {
            _invitationManagement = invitationManagement;
        }

        public Invitation InsertInvitation(Invitation invitation)
        {
            if (IsInvitationValid(invitation))
            {
                // Se agrega la Invitation a la DB: _invitationManagement.InsertInvitation(invitation);
                FakeDB.Invitations.Add(invitation);
            }
            return invitation;
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
            if (invitation.Pharmacy == null)
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
            foreach (Invitation invitation in FakeDB.Invitations)
            {
                if (invitation.IdInvitation.Equals(idInvitation))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsNameRegistered(string name)
        {
            // Aquí se recorren los usuarios YA REGISTRADOS, para no hacer una invitación con un nombre que ya existe.
            foreach (User user in FakeDB.Users)
            {
                if (name.Equals(user.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCodeRegistered(int code) 
        {
            // Aquí se recorren los usuarios YA REGISTRADOS, para no hacer una invitación con un código que ya existe.
            foreach (User user in FakeDB.Users)
            {
                if (code.Equals(user.Code))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
