﻿using Microsoft.EntityFrameworkCore;
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
            Pharmacy pharmacyOfInvitation = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(invitation.Pharmacy.Name)).FirstOrDefault();
            if (pharmacyOfInvitation != null)
            {
                ObgContext.Attach(invitation.Pharmacy);
            }
            ObgContext.Invitations.Add(invitation);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            IEnumerable<Invitation> list = ObgContext.Invitations.ToList();
            List<Invitation> ret = new List<Invitation>();
            foreach (Invitation inv in list)
            {
                Invitation invAdd = ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(inv.IdInvitation)).Include("Pharmacy").FirstOrDefault();
                ret.Add(invAdd);
            }

            return ret;

        }

        public Invitation GetInvitationById(string id)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(id)).Include("Pharmacy").FirstOrDefault();
        }

        public Invitation GetInvitationByCode(int code)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).Include("Pharmacy").FirstOrDefault();
        }

        public Invitation GetInvitationByUserName(string userName)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.UserName.Equals(userName)).FirstOrDefault();
        }

        public Invitation GetInvitationByAdministratorName(string administratorName)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.AdministratorName.Equals(administratorName)).FirstOrDefault();
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
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(idInvitation)).FirstOrDefault();
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
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserName.Equals(name)).FirstOrDefault();
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
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).FirstOrDefault();
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