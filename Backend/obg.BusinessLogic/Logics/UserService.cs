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
        private readonly IAdministratorManagement _administratorManagement;
        private readonly IOwnerManagement _ownerManagement;
        private readonly IEmployeeManagement _employeeManagement;
        private readonly IInvitationManagement _invitationManagement;

        public UserService() { }
        public UserService(IUserManagement userManagement, IAdministratorManagement administratorManagement, IOwnerManagement ownerManagement, IEmployeeManagement employeeManagement, IInvitationManagement invitationManagement)
        {
            _userManagement = userManagement;
            _administratorManagement = administratorManagement;
            _ownerManagement = ownerManagement;
            _employeeManagement = employeeManagement;
            _invitationManagement = invitationManagement;
        }

        public string InsertUser(User user)
        {
            if (ExistsInvitation(user.Name, user.Code))
            {
                user.Role = GetRole(user.Code);
                SetDefaultUserPreRegister(user);
                if (IsUserValid(user))
                {
                    if (user.Role == RoleUser.Administrator)
                    {
                        Administrator administrator = ParseToAdministrator(user);
                        _administratorManagement.InsertAdministrator(administrator);
                    }
                    else if (user.Role == RoleUser.Owner)
                    {
                        Owner owner = ParseToOwner(user);
                        Invitation invitation = _invitationManagement.GetInvitationByCode(owner.Code);
                        Pharmacy pharmacy = invitation.Pharmacy;
                        owner.Pharmacy = pharmacy;
                        if (HasAPharmacy(owner))
                        {
                            _ownerManagement.InsertOwner(owner);
                        }
                    }
                    else
                    {
                        Employee employee = ParseToEmployee(user);
                        Invitation invitation = _invitationManagement.GetInvitationByCode(employee.Code);
                        Pharmacy pharmacy = invitation.Pharmacy;
                        employee.Pharmacy = pharmacy;
                        if (HasAPharmacy(employee))
                        {
                            _employeeManagement.InsertEmployee(employee);
                        }
                    }
                }
            }
            else
            {
                throw new NotFoundException("No existe una invitación para el usuario");
            }
            Invitation invitationToUse = _invitationManagement.GetInvitationByCode(user.Code);
            invitationToUse.WasUsed = true;
            _invitationManagement.UpdateInvitation(invitationToUse); // Hago que la invitación fue utilizada.
            return user.Name;
        }

        private bool ExistsInvitation(string name, int code)
        {
            Invitation invitation = _invitationManagement.GetInvitationByCode(code);
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

        private RoleUser GetRole(int code)
        {
            Invitation invitation = _invitationManagement.GetInvitationByCode(code);
            return invitation.UserRole;
        }

        private void SetDefaultUserPreRegister(User user)
        {
            user.Email = Guid.NewGuid().ToString().Substring(0, 10) + "@gmail.com";
            user.Password = Guid.NewGuid().ToString().Substring(0, 10) + ".44#";
            user.Address = "Default Address";
            user.RegisterDate = "Default RegisterDate";
        }

        public string UpdateUser(User user, string userName)
        {
            //User userFromDB = _userManagement.GetUserByName(user.Name);
            User userFromDB = _userManagement.GetUserByName(userName);
            if (userFromDB == null)
            {
                throw new NotFoundException("El usuario no existe.");
            }
            else
            {
                userFromDB.Email = user.Email;
                userFromDB.Password = user.Password;
                userFromDB.Address = user.Address;
                userFromDB.RegisterDate = DateTime.Now.ToShortDateString();
                if (IsUpdateUserValid(userFromDB))
                {
                    _userManagement.UpdateUser(userFromDB);
                }
            }

            return userName;
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

        protected bool IsUpdateUserValid(User user)
        {
            if (user.Email == null || user.Email.Length == 0)
            {
                throw new UserException("Email inválido.");
            }
            if (!IsEmailOK(user.Email))
            {
                throw new UserException("Email con formato inválido.");
            }
            if (!IsPasswordOK(user.Password))
            {
                throw new UserException("Contraseña con formato inválida.");
            }
            if (user.Address == null || user.Address.Length == 0)
            {
                throw new UserException("Dirección inválida.");
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

        private Administrator ParseToAdministrator(User user)
        {
            Administrator administrator = new Administrator();
            administrator.Name = user.Name;
            administrator.Code = user.Code;
            administrator.Email = user.Email;
            administrator.Password = user.Password;
            administrator.Address = user.Address;
            administrator.Role = RoleUser.Administrator;
            administrator.RegisterDate = user.RegisterDate;
            administrator.Pharmacies = new List<Pharmacy>();
            return administrator;
        }

        private Owner ParseToOwner(User user)
        {
            Owner owner = new Owner();
            owner.Name = user.Name;
            owner.Code = user.Code;
            owner.Email = user.Email;
            owner.Password = user.Password;
            owner.Address = user.Address;
            owner.Role = RoleUser.Owner;
            owner.RegisterDate = user.RegisterDate;
            owner.Pharmacy = new Pharmacy();
            return owner;
        }

        private Employee ParseToEmployee(User user)
        {
            Employee employee = new Employee();
            employee.Name = user.Name;
            employee.Code = user.Code;
            employee.Email = user.Email;
            employee.Password = user.Password;
            employee.Address = user.Address;
            employee.Role = RoleUser.Employee;
            employee.RegisterDate = user.RegisterDate;
            employee.Pharmacy = new Pharmacy();
            return employee;
        }

        private bool HasAPharmacy(Owner owner)
        {
            if (owner.Pharmacy == null)
            {
                throw new UserException("El dueño no tiene una farmacia asignada.");
            }
            return true;
        }

        private bool HasAPharmacy(Employee employee)
        {
            if (employee.Pharmacy == null)
            {
                throw new UserException("El empleado no tiene una farmacia asignada.");
            }
            return true;
        }

    }
}
