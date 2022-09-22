using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Invitation
    {
        public int IdInvitation { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public RoleUser UserRole { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }

        public Invitation(int idInvitation, Pharmacy pharmacy, RoleUser userRole, string userName, string userCode)
        {
            IdInvitation = idInvitation;
            Pharmacy = pharmacy;
            UserRole = userRole;
            UserName = userName;
            UserCode = userCode;
        }
    }
}
