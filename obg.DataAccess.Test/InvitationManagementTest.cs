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
    }
}
