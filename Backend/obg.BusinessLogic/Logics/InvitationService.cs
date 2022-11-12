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

        public string UpdateInvitation(string idInvitation, Invitation invitation, string token)
        {
            if (!invitation.WasUsed)
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                string administratorName = session.UserName;
                Invitation invitationFromDB = _invitationManagement.GetInvitationByAdministratorName(administratorName);
                invitationFromDB.IdInvitation = invitation.IdInvitation;
                invitationFromDB.UserName = invitation.UserName;
                invitationFromDB.UserCode = invitation.UserCode;
                invitationFromDB.UserRole = invitation.UserRole;
                invitationFromDB.WasUsed = invitation.WasUsed;
                invitationFromDB.Pharmacy = invitation.Pharmacy;
                if (IsInvitationValid(invitation))
                {
                    _invitationManagement.UpdateInvitation(invitationFromDB);
                }
                return invitationFromDB.IdInvitation.ToString();
            }
            throw new InvitationException("No se puede modificar una invitación que fue usada.");
        }

        public IEnumerable<Invitation> GetInvitations(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            string administratorName = session.UserName;
            IEnumerable<Invitation> invitations = _invitationManagement.GetInvitations();
            if(invitations.ToList().Count == 0 || invitations == null)
            {
                throw new NotFoundException("No hay invitaciones enviadas.");
            }

            //List<Invitation> invitationsUsed = new List<Invitation>();
            //foreach (Invitation invitation in invitations)
            //{
            //    if (invitation.WasUsed)
            //    {
            //        invitationsUsed.Add(invitation);
            //    }
            //}
            //if(invitationsUsed.Count == 0)
            //{
            //    throw new NotFoundException("No hay invitaciones enviadas.");
            //}
            //return invitationsUsed;
            return invitations;
        }

        public Invitation GetInvitationById(string id)
        {
            return _invitationManagement.GetInvitationById(id);
        }

        // public void UpdateInvitation(Invitation invitation)
        // {
        //     _invitationManagement.UpdateInvitation(invitation);
        // }

        public int InsertInvitation(Invitation invitation, string pharmacyName)
        {
            IEnumerable<Invitation> invitationsFromAdministrator = new List<Invitation>();
            foreach(Invitation invitation in invitations)
            {
                if (invitation.AdministratorName.Equals(administratorName))
                {
                    invitationsFromAdministrator.ToList().Add(invitation);
                }
            }
            if (invitationsFromAdministrator.ToList().Count == 0 || invitationsFromAdministrator == null)
            {
                throw new NotFoundException("No hay invitaciones enviadas.");
            }
            return invitationsFromAdministrator;
        }

        public int InsertInvitation(Invitation invitation, string pharmacyName, string token)
        {
            Pharmacy pharmacy = _pharmacyManagement.GetPharmacyByName(pharmacyName);
            Session session = _sessionManagement.GetSessionByToken(token);
            string administratorName = session.UserName;
            bool noAdministrator = invitation.UserRole != 0;
            if (noAdministrator && pharmacy == null)
            {
                throw new NotFoundException("No existe la farmacia.");
            }
            Console.WriteLine("Llegó hasta aquí.");
            Console.WriteLine(pharmacy);
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
