﻿using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace obg.Domain.Entities
{
    public class Invitation
    {
        [Key] public string IdInvitation { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public RoleUser UserRole { get; set; }
        public string UserName { get; set; }
        public int UserCode { get; set; }
        public bool WasUsed { get; set; } 
        public string AdministratorName { get; set; }

        public Invitation() { }
        public Invitation(string idInvitation, Pharmacy pharmacy, RoleUser userRole, string userName, int userCode, string administratorName)
        {
            IdInvitation = idInvitation;
            Pharmacy = pharmacy;
            UserRole = userRole;
            UserName = userName;
            UserCode = userCode;
            WasUsed = false;
            AdministratorName = administratorName;
        }
    }
}
