using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IInvitationManagement
    {
        public void InsertInvitation(Invitation invitation);
    }
}
