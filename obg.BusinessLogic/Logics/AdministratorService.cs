using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace obg.BusinessLogic.Logics
{
    public class AdministratorService
    {
        private readonly IAdministratorManagement _administratorManagement;
        private List<Administrator> fakeDB = new List<Administrator>();
        public AdministratorService(IAdministratorManagement administratorManagement)
        {
            _administratorManagement = administratorManagement;
        }

        public Administrator InsertAdministrator(Administrator administrator)
        {
            if (IsAdministratorValid(administrator))
            {
                fakeDB.Add(administrator);
            }
            return administrator;
        }
        
        private bool IsAdministratorValid(Administrator administrator)
        {
            if (administrator == null)
            {
                throw new AdministratorException("Administrador inválido.");
            }
            if (administrator.Name == null || administrator.Name.Length == 0 || administrator.Name.Length > 20)
            {
                throw new AdministratorException("Nombre inválido.");
            }
            if (IsNameRegistered(administrator.Name))
            {
                throw new AdministratorException("El nombre ya fue registrado.");
            }
            if (administrator.Code == null || administrator.Code.Length == 0)
            {
                throw new AdministratorException("Código inválido.");
            }
            if (IsCodeRegistered(administrator.Code))
            {
                throw new AdministratorException("El nombre ya fue registrado.");
            }
            if (administrator.Email == null || administrator.Email.Length == 0)
            {
                throw new AdministratorException("Email inválido.");
            }
            if (!IsEmailOK(administrator.Email))
            {
                throw new AdministratorException("Email con formato inválido.");
            }
            if (IsEmailRegistered(administrator.Email))
            {
                throw new AdministratorException("El nombre ya fue registrado.");
            }
            if (!IsPasswordOK(administrator.Password))
            {
                throw new AdministratorException("Contraseña con formato inválida.");
            }
            return true;
        }


        private bool IsNameRegistered(string name)
        {
            foreach(Administrator administrator in this.fakeDB)
            {
                if (name.Equals(administrator.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCodeRegistered(string code)
        {
            foreach (Administrator administrator in this.fakeDB)
            {
                if (code.Equals(administrator.Code))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsEmailRegistered(string email)
        {
            foreach (Administrator administrator in this.fakeDB)
            {
                if (email.Equals(administrator.Email))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsPasswordOK(string password)
        {
            Regex specialCharacters = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]^_`{|}~]");
            if (password != null && !password.Equals("") && password.Length >= 8 && specialCharacters.IsMatch(password))
            {
                return true;
            }
            return false;
        }

        // Font: https://stackoverflow.com/questions/5342375/regex-email-validation
        public bool IsEmailOK(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
