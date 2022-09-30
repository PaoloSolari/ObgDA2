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
            ObgContext.Invitations.Add(invitation);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            return ObgContext.Invitations.ToList();
        }

        public Invitation GetInvitationById(string id)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation == id).AsNoTracking().FirstOrDefault();
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

    }
}