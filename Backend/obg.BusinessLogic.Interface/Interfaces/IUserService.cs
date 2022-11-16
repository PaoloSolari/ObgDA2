using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IUserService
    {
        string InsertUser(User user);
        string UpdateUser(User user, string userName);
        User GetUserByName(string userName);
    }
}
