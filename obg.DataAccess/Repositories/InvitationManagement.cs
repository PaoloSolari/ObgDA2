using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace obg.DataAccess.Repositories
{
    public class InvitationManagement : IInvitationManagement
    {
        private ObgContext ObgContext { get; set; }
        public InvitationManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertInvitation(Invitation invitation)
        {
            // Cuando insertás una invitación, se inserta la farmacia que tiene relacionada.
            // Si existe la farmacia, tratas de no agregarla nuevamente.
            //Pharmacy pharmacyOfInvitation = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(invitation.Pharmacy.Name)).AsNoTracking().FirstOrDefault();
            //if (pharmacyOfInvitation != null)
            //{
            //    invitation.Pharmacy = null;
            //}
            ObgContext.Invitations.Add(invitation);
            ObgContext.SaveChanges();
            //ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(invitation.IdInvitation)).AsNoTracking().FirstOrDefault().Pharmacy = pharmacyOfInvitation;
            //invitationInsertered.Pharmacy = pharmacyOfInvitation;
            //ObgContext.SaveChanges();
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            return ObgContext.Invitations.ToList();
        }

        public Invitation GetInvitationById(string id)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(id)).AsNoTracking().FirstOrDefault();
        }

        public Invitation GetInvitationByCode(int code)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).Include("Pharmacy").AsNoTracking().FirstOrDefault();
        }
        public void UpdateInvitation(Invitation invitation)
        {
            ObgContext.Invitations.Attach(invitation);
            ObgContext.Entry(invitation).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteInvitation(Invitation invitation)
        {
            ObgContext.Invitations.Remove(invitation);
            ObgContext.SaveChanges();
        }

        public bool IsIdInvitationRegistered(string idInvitation)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(idInvitation)).AsNoTracking().FirstOrDefault();
            if(invitation != null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public bool IsNameRegistered(string name)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserName.Equals(name)).AsNoTracking().FirstOrDefault();
            if(invitation != null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public bool IsCodeRegistered(int code)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).AsNoTracking().FirstOrDefault();
            if (invitation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}