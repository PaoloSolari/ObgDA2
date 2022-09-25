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
        //private readonly IAdministratorManagement _administratorManagement;

        //public AdministratorService(IAdministratorManagement administratorManagement)
        //{
        //    _administratorManagement = administratorManagement;
        //}

        //public AdministratorService()
        //{

        //}

        public Administrator InsertAdministrator(Administrator administrator)
        {
            if (IsUserValid(administrator) && IsAAdministrator(administrator))
            {
                // Se agreaga el Administrator a la DB: _administratorManagement.InsertEmployee(employee);
                fakeDB.Add(administrator);
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

            return fakeDB;
        }


        //public Administrator GetAdministratorById(int id)
        //{

        //    Administrator auxAdministrator = null;
        //    foreach (Administrator administrator in fakeDB)
        //    {
        //        if (administrator.Id == id)
        //        {
        //            auxAdministrator = administrator;
        //        }
        //    }
        //    if (auxAdministrator == null)
        //    {
        //        throw new UserException("La farmacia no existe.");
        //    }
        //    return auxAdministrator;
        //}

    }
}
