using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IAdministratorService
    {
        IEnumerable<User> GetAdministrators();
        Administrator InsertAdministrator(Administrator administrator);
        Administrator UpdateAdministrator(Administrator administrator);
    }
}
