using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
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

        public AdministratorService() { }

        public Administrator InsertAdministrator(Administrator administrator)
        {
            if (IsAnAdministrator(administrator))
            {
                _administratorManagement.InsertAdministrator(administrator);
            }
            return administrator;
        }

        private bool IsAnAdministrator(Administrator administrator)
        {
            if (administrator.Role != RoleUser.Administrator)
            {
                throw new UserException("El admnistrador tiene asignado un rol incorrecto.");
            }
            return true;
        }

        public IEnumerable<User> GetAdministrators()
        {
            return _administratorManagement.GetAdministrators();
        }

        public Administrator UpdateAdministrator(Administrator administratorToUpdate)
        {
            if (IsUserValid(administratorToUpdate) && IsAnAdministrator(administratorToUpdate))
            {
                Administrator administrator = _administratorManagement.GetAdministratorByName(administratorToUpdate.Name);
                if (administrator == null)
                {
                    throw new NotFoundException("El administrador no existe.");
                }
                _administratorManagement.UpdateAdministrator(administratorToUpdate);
            }
            return administratorToUpdate;
        }


        public Administrator GetAdministratorByName(string name)
        {
            Administrator administrator = _administratorManagement.GetAdministratorByName(name);
            if (administrator == null)
            {
                throw new NotFoundException("El administrador no existe");
            }
            return administrator;
        }

        //public Administrator ConvertToAdministrator(User user)
        //{
        //    Administrator administrator = new Administrator();
        //    administrator.Name = user.Name;
        //    administrator.Code = user.Code;
        //    administrator.Email = user.Email;
        //    administrator.Password = user.Password;
        //    administrator.Address = user.Address;
        //    administrator.Role = user.Role;
        //    administrator.RegisterDate = user.RegisterDate;
        //    administrator.Pharmacies = new List<Pharmacy>();
        //    return administrator;
        //}
    }
}
