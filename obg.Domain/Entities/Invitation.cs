using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Invitation
    {
        public string IdInvitation { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public RoleUser UserRole { get; set; }
        public string UserName { get; set; }
        public int UserCode { get; set; }

        public Invitation(string idInvitation, Pharmacy pharmacy, RoleUser userRole, string userName, int userCode)
        {
            IdInvitation = idInvitation;
            Pharmacy = pharmacy;
            UserRole = userRole;
            UserName = userName;
            UserCode = userCode;
            InvitationCode = new Guid().ToString().Substring(0, 6);
        }

        public Invitation() { }
    }
}
