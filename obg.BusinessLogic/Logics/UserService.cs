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
    public class UserService
    {
        protected List<User> fakeDB = new List<User>();

        public UserService() { }

        protected bool IsUserValid(User user)
        {
            if (user == null)
            {
                throw new UserException("Usuario inválido.");
            }
            if (user.Name == null || user.Name.Length == 0 || user.Name.Length > 20)
            {
                throw new UserException("Nombre inválido.");
            }
            if (IsNameRegistered(user.Name))
            {
                throw new UserException("El nombre ya fue registrado.");
            }
            if (user.Code == null || user.Code.Length == 0)
            {
                throw new UserException("Código inválido.");
            }
            if (IsCodeRegistered(user.Code))
            {
                throw new UserException("El nombre ya fue registrado.");
            }
            if (user.Email == null || user.Email.Length == 0)
            {
                throw new UserException("Email inválido.");
            }
            if (!IsEmailOK(user.Email))
            {
                throw new UserException("Email con formato inválido.");
            }
            if (IsEmailRegistered(user.Email))
            {
                throw new UserException("El nombre ya fue registrado.");
            }
            if (!IsPasswordOK(user.Password))
            {
                throw new UserException("Contraseña con formato inválida.");
            }
            if (user.Address == null || user.Address.Length == 0)
            {
                throw new UserException("Dirección inválida.");
            }
            if (user.RegisterDate == null || user.RegisterDate.Length == 0)
            {
                throw new UserException("Fecha de registro inválida.");
            }
            return true;
        }

        private bool IsNameRegistered(string name)
        {
            foreach (User user in this.fakeDB)
            {
                if (name.Equals(user.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCodeRegistered(string code)
        {
            foreach (User user in this.fakeDB)
            {
                if (code.Equals(user.Code))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsEmailRegistered(string email)
        {
            foreach (User user in this.fakeDB)
            {
                if (email.Equals(user.Email))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsPasswordOK(string password)
        {
            Regex specialCharacters = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]^_`{|}~]");
            if (password != null && !password.Equals("") && password.Length >= 8 && specialCharacters.IsMatch(password))
            {
                return true;
            }
            return false;
        }

        // Font: https://stackoverflow.com/questions/5342375/regex-email-validation
        protected bool IsEmailOK(string email)
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
