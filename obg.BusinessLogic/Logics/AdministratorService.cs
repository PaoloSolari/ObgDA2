using obg.BusinessLogic.Interface.Interfaces;
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
    public class AdministratorService : UserService, IAdministratorService
    {
        private readonly IAdministratorManagement _administratorManagement;

        public AdministratorService(IAdministratorManagement administratorManagement)
        {
            _administratorManagement = administratorManagement;
        }

        public AdministratorService()
        {
            //validAdministrator = new Administrator("Paolo", 123456, "ps@gmail.com", "password123.", "addressPS", RoleUser.Administrator, "12/09/2022", null);
            //fakeDB.Add(validAdministrator);
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

        public IEnumerable<User> GetAdministrators()
        {
            //return _pharmacyManagement.GetPharmacies();

            return FakeDB.Administrators;
        }

        public Administrator UpdateAdministrator(Administrator administratorToUpdate)
        {

            Administrator administrator = GetAdministratorByName(administratorToUpdate.Name);
            return administrator;
        }


        public Administrator GetAdministratorByName(string name)
        {

            Administrator auxAdministrator = null;
            foreach (Administrator administrator in FakeDB.Administrators)
            {
                if (administrator.Name.Equals(name))
                {
                    auxAdministrator = administrator;
                }
            }
            if (auxAdministrator == null)
            {
                throw new UserException("El administrador no existe.");
            }
            return auxAdministrator;
        }

    }
}
