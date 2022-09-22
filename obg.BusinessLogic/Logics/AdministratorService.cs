using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
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
            if (IsUserValid(administrator) && IsAAdministrator(administrator))
            {
                // Se agrega el Administrator a la DB: _administratorManagement.InsertEmployee(employee);
                FakeDB.Administrators.Add(administrator);
                FakeDB.Users.Add(administrator);
            }
            return administrator;
        }

        private bool IsAAdministrator(Administrator administrator)
        {
            if (administrator.Role != RoleUser.Administrator)
            {
                throw new UserException("El admnistrador tiene asignado un rol incorrecto.");
            }
            return true;
        }

    }
}
