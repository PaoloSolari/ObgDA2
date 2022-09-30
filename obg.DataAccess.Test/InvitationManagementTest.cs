using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class InvitationManagementTest
    {
        private Pharmacy pharmacy;
        private Invitation invitation;
        private List<Invitation> invitations;

        [TestInitialize]
        public void InitTest()
        {
            pharmacy = new Pharmacy("FarmaShop", "18 de Julio", null);
            invitation = new Invitation("XXZZYY", pharmacy, RoleUser.Owner, "Pedro", 343434);

            invitations = new List<Invitation>() { invitation };
        }

        [TestMethod]
        public void InsertInvitationOk()
        {
            ObgContext context = CreateContext();
            IInvitationManagement invitationManagement = new InvitationManagement(context);

            invitationManagement.InsertInvitation(invitation);

            Invitation invitationInDatabase = context.Invitations.Where<Invitation>(i => i.IdInvitation == invitation.IdInvitation).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(invitationInDatabase);
            Assert.AreEqual(invitationInDatabase.IdInvitation, invitation.IdInvitation);
        }

        [TestMethod]
        public void GetInvitationsOk()
        {
            IInvitationManagement invitationManagement = CreateInvitationManagement();
            IEnumerable<Invitation> invitationsInDatabase = invitationManagement.GetInvitations();

            Assert.AreEqual(invitationsInDatabase.ToList().Count, invitations.Count);
            Assert.AreEqual(invitationsInDatabase.ToList()[0].IdInvitation, invitations[0].IdInvitation);

        }

        [TestMethod]
        public void GetInvitationByIdOk()
        {
            ObgContext context = CreateContext();
            IInvitationManagement invitationManagement = new InvitationManagement(context);

            context.Invitations.Add(invitation);
            context.SaveChanges();

            Invitation invitationInDatabase = invitationManagement.GetInvitationById(invitation.IdInvitation);

            Assert.IsNotNull(invitationInDatabase);
            Assert.AreEqual(invitationInDatabase.IdInvitation, invitation.IdInvitation);
        }

        [TestMethod]
        public void UpdateInvitationOk()
        {
            ObgContext context = CreateContext();
            IInvitationManagement invitationManagement = new InvitationManagement(context);

            context.Invitations.Add(invitation);
            context.SaveChanges();
            invitation.UserRole = RoleUser.Employee;
            invitationManagement.UpdateInvitation(invitation);

            Invitation invitationInDatabase = context.Invitations.Where<Invitation>(d => d.IdInvitation == invitation.IdInvitation).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(invitationInDatabase);
            Assert.AreEqual(invitationInDatabase.UserRole, invitation.UserRole);
        }

        [TestMethod]
        public void DeleteInvitationOk()
        {
            ObgContext context = CreateContext();
            IInvitationManagement invitationManagement = new InvitationManagement(context);

            context.Invitations.Add(invitation);
            context.SaveChanges();

            invitationManagement.DeleteInvitation(invitation);

            Invitation invitationInDatabase = context.Invitations.Where<Invitation>(d => d.IdInvitation == invitation.IdInvitation).AsNoTracking().FirstOrDefault();

            Assert.IsNull(invitationInDatabase);
        }

        private ObgContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ObgContext>()
                .UseInMemoryDatabase("ObgDA2")
                .Options;

            var context = new ObgContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        private IInvitationManagement CreateInvitationManagement()
        {
            var context = CreateContext();

            context.Invitations.Add(invitation);
            context.SaveChanges();

            IInvitationManagement invitationManagement = new InvitationManagement(context);
            return invitationManagement;
        }

    }
}
