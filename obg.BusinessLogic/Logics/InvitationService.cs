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
        //private readonly IInvitationManagement _invitationManagement;

        //public InvitationService(IInvitationManagement invitationManagement)
        //{
        //    _invitationManagement = invitationManagement;
        //}

        public Invitation InsertInvitation(Invitation invitation)
        {
            if (IsInvitationValid(invitation))
            {
                // Se agreaga la Invitation a la DB: _invitationManagement.InsertInvitation(invitation);
                fakeDB.Add(invitation);
            }
            return invitation;
        }

        private bool IsInvitationValid(Invitation invitation)
        {
            if (invitation == null) throw new InvitationException("Invitación inválida.");
            if (invitation.Pharmacy == null) throw new InvitationException("Farmacia inválida.");
            if (invitation.UserName == null || invitation.UserName.Length == 0) throw new InvitationException("Nombre de usuario inválido.");
            if (invitation.UserCode == null || invitation.UserCode.Length == 0) throw new InvitationException("Código de usuario inválido.");

            return true;
        }
    }
}
