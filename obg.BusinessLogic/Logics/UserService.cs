using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace obg.BusinessLogic.Logics
{
    public class UserService : IUserService
    {
        private readonly IUserManagement _userManagement;

        public UserService(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public UserService() { }

        public string InsertUser(User user)
        {
            if (ExistsInvitation(user.Name, user.Code))
            {
                SetDefaultUserPreRegister(user);
                if (IsUserValid(user))
                {
                    _userManagement.InsertUser(user);
                }
            }
            return user.Name;
        }

        private bool ExistsInvitation(string name, int code)
        {
            Invitation invitation = _userManagement.GetInvitationByCode(code);
            if (invitation == null)
            {
                return false;
            }
            if (invitation.UserName.Equals(name))
            {
                return true;
            }
            return false;
        }

        private void SetDefaultUserPreRegister(User user)
        {
            Random random = new Random();
            string ramdomString = random.Next(0, 1000000).ToString("D6");
            int randomInt = Int32.Parse(ramdomString);
            user.Code = randomInt;
            user.Email = Guid.NewGuid().ToString().Substring(0, 10) + "@gmail.com";
            user.Password = Guid.NewGuid().ToString().Substring(0, 10) + ".44#";
            user.Address = "Default Address";
            user.RegisterDate = "Default RegisterDate";
    }

        public string UpdateUser(User user)
        {
            User userFromDB = _userManagement.GetUserByName(user.Name);
            if(userFromDB == null)
            {
                throw new NotFoundException();
            }
            else
            {
                userFromDB.Email = user.Email;
                userFromDB.Password = user.Password;
                userFromDB.Address = user.Address;
                _userManagement.UpdateUser(user);
            }

            return user.Name;
        }

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
            if(user.Code.ToString("D6").Length != 6)
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
            User userFromDB = _userManagement.GetUserByName(name);
            if(userFromDB == null)
            {
                return false;
            }
            return true;
        }

        private bool IsCodeRegistered(int code)
        {
            IEnumerable<User> usersFromDB = _userManagement.GetUsers();
            foreach (User userInDB in usersFromDB)
            {
                if(userInDB.Code == code)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsEmailRegistered(string email)
        {
            IEnumerable<User> usersFromDB = _userManagement.GetUsers();
            foreach (User userInDB in usersFromDB)
            {
                if (userInDB.Email.Equals(email))
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
