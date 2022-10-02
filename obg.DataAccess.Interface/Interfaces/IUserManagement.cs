using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IUserManagement
    {
        void InsertUser(User user);
        IEnumerable<User> GetUsers();
        User GetUserByName(string name);
        void UpdateUser(User user);
        Invitation GetInvitationByCode(int code);
    }
}
