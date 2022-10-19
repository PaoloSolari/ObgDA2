using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Interface.Interfaces
{
    public interface IAdministratorService
    {
        IEnumerable<User> GetAdministrators();
        string InsertAdministrator(Administrator administrator);
        string UpdateAdministrator(Administrator administrator);
    }
}
