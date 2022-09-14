using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace obg.BusinessLogic.Logics
{
    public class AdministratorService : UserService
    {
        private readonly IAdministratorManagement _administratorManagement;

        public AdministratorService(IAdministratorManagement administratorManagement)
        {
            _administratorManagement = administratorManagement;
        }

        public Administrator InsertAdministrator(Administrator administrator)
        {
            if (IsUserValid(administrator))
            {
                // Se agreaga el Administrator a la DB: _administratorManagement.InsertEmployee(employee);
                fakeDB.Add(administrator);
            }
            return administrator;
        }
        
    }
}
