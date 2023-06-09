﻿using obg.BusinessLogic.Interface;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Domain.Enums;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class SessionService : ISessionService
    {
        private readonly ISessionManagement _sessionManagement;
        private readonly IUserManagement _userManagement;

        public SessionService(ISessionManagement sessionManagement, IUserManagement userManagement)
        {
            _sessionManagement = sessionManagement;
            _userManagement = userManagement;
        }

        public Session GetSessionByToken(string token)
        {
            return _sessionManagement.GetSessionByToken(token);
        }

        public Session GetSessionByName(string userName)
        {
            return _sessionManagement.GetSessionByName(userName);
        }

        public string InsertSession(Session session, string password)
        {
            if (IsSessionValid(session, password))
            {
                session.IdSession = CreateGuid();
                session.Token = CreateGuid();
                _sessionManagement.InsertSession(session);
            }
            return session.Token;
        }

        private string CreateGuid()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }

        private bool IsSessionValid(Session session, string password)
        {
            if (session == null)
            {
                throw new SessionException("Sesión inválida.");
            }
            if (session.UserName == null || session.UserName.Length == 0 || session.UserName.Length > 20)
            {
                throw new SessionException("Nombre de usuario inválido.");
            }
            if (!IsUserOk(session.UserName, password))
            {
                throw new SessionException("Contraseña incorrecta.");
            }
            return true;
        }

        public bool IsIdSessionRegistered(string idSession)
        {
            return _sessionManagement.IsIdSessionRegistered(idSession);
        }

        private bool IsUserOk(string userName, string password)
        {
            User userFromDB = _userManagement.GetUserByName(userName);
            if(userFromDB != null)
            {
                if (userFromDB.Password.Equals(password))
                {
                    return true;
                }
            }
            else
            {
                throw new NotFoundException("No existe el usuario");
            }

            return false;
        }

        public bool IsTokenValid(string token)
        {
            return _sessionManagement.IsTokenValid(token);
        }

        public RoleUser GetUserRole(string token)
        {
            Session session = _sessionManagement.GetSessionByToken(token);
            if(session == null)
            {
                throw new NotFoundException("Sesión inválida.");
            } 
            else
            {
                string userName = session.UserName;
                User user = _userManagement.GetUserByName(userName);
                if(user == null)
                {
                    throw new NotFoundException("Usuario de sesión incorrecto.");
                }
                return user.Role;
            }
        }

    }
}
