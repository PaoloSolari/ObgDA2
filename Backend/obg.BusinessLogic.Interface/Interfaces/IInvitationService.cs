using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IInvitationService
    {
        //int InsertInvitation(Invitation invitation, string pharamacyName);
        //IEnumerable<Invitation> GetInvitations();
        Invitation GetInvitationById(string id);
        void UpdateInvitation(Invitation invitation, string token);
        int InsertInvitation(Invitation invitation, string pharamacyName, string token);
        IEnumerable<Invitation> GetInvitations(string token);
        //string UpdateInvitation(string idInvitation, Invitation invitation, string token);
    }
}
