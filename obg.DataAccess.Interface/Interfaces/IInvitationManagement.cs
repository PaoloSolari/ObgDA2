using obg.Domain.Entities;
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
        void UpdateInvitation(Invitation invitation);
        void DeleteInvitation(Invitation invitation);
    }
}
