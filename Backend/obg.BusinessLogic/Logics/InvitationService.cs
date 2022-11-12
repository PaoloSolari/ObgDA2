using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationManagement _invitationManagement;
        private readonly IPharmacyManagement _pharmacyManagement;
        private readonly ISessionManagement _sessionManagement;
        private readonly IAdministratorManagement _administratorManagement;

        public InvitationService(IInvitationManagement invitationManagement, IPharmacyManagement pharmacyManagement, ISessionManagement sessionManagement, IAdministratorManagement administratorManagement)
        {
            _invitationManagement = invitationManagement;
            _pharmacyManagement = pharmacyManagement;
            _sessionManagement = sessionManagement;
            _administratorManagement = administratorManagement;
        }

        public IEnumerable<Invitation> GetInvitations(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            string administratorName = session.UserName;
            IEnumerable<Invitation> invitations = _invitationManagement.GetInvitations();
            if (invitations.ToList().Count == 0 || invitations == null)
            {
                throw new NotFoundException("No hay invitaciones enviadas.");
            }
            List<Invitation> invitationsFromAdministrator = new List<Invitation>();
            foreach (Invitation invitation in invitations)
            {
                if (invitation.AdministratorName.Equals(administratorName))
                {
                    invitationsFromAdministrator.Add(invitation);
                }
            }
            if (invitationsFromAdministrator.Count == 0 || invitationsFromAdministrator == null)
            {
                throw new NotFoundException("No hay invitaciones enviadas.");
            }
            return invitationsFromAdministrator;
        }


        public Invitation GetInvitationById(string id)
        {
            return _invitationManagement.GetInvitationById(id);
        }

        public void UpdateInvitation(Invitation invitation, string token)
        {
            if (IsInvitationValid(invitation))
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                string administratorName = session.UserName;
                invitation.AdministratorName = administratorName;
                _invitationManagement.UpdateInvitation(invitation);
            }
        }

        public int InsertInvitation(Invitation invitation, string pharmacyName, string token)
        {
            Pharmacy pharmacy = _pharmacyManagement.GetPharmacyByName(pharmacyName);
            Session session = _sessionManagement.GetSessionByToken(token);
            string administratorName = session.UserName;
            if (pharmacy == null)
            {
                throw new NotFoundException("No existe la farmacia.");
            }
            invitation.IdInvitation = CreateId();
            invitation.Pharmacy = pharmacy;
            invitation.UserCode = CreateCode();
            invitation.AdministratorName = administratorName;

            if (IsInvitationValid(invitation))
            {
                _invitationManagement.InsertInvitation(invitation);
            }
            return invitation.UserCode;
        }

        public Administrator GetInvitationAdministrator(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            string administratorName = session.UserName;
            Administrator administrator = _administratorManagement.GetAdministratorByName(administratorName);
            return administrator;
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
                throw new InvitationException("El código de usuario ya fue registrado.");
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
