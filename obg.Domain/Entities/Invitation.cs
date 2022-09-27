using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Invitation
    {
        public Pharmacy Pharmacy { get; set; }
        public RoleUser UserRole { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string InvitationCode { get; set; }

        public Invitation(Pharmacy pharmacy, RoleUser userRole, string userName, string userCode)
        {
            Pharmacy = pharmacy;
            UserRole = userRole;
            UserName = userName;
            UserCode = userCode;
            InvitationCode = new Guid().ToString().Substring(0, 6);
        }

        public Invitation() { }
    }
}
