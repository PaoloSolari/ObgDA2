﻿using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IInvitationManagement
    {
        void InsertInvitation(Invitation invitation);
        IEnumerable<Invitation> GetInvitations();
        Invitation GetInvitationById(string id);
        Invitation GetInvitationByCode(int code);
        Invitation GetInvitationByUserName(string userName);
        Invitation GetInvitationByAdministratorName(string administratorName);
        void UpdateInvitation(Invitation invitation);
        void DeleteInvitation(Invitation invitation);
        bool IsIdInvitationRegistered(string idInvitation);
        bool IsNameRegistered(string name);
        bool IsCodeRegistered(int code);
    }
}
