using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IAdministratorManagement
    {
        void InsertAdministrator(Administrator administrator);
        IEnumerable<Administrator> GetAdministrators();
        Administrator GetAdministratorByName(string name);
        void UpdateAdministrator(Administrator admnistrator);
        void DeleteAdministrator(Administrator admnistrator);
    }
}
