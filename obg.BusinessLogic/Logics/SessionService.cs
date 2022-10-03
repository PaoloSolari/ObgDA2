using obg.BusinessLogic.Interface;
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

        public string InsertSession(Session session)
        {
            if (IsSessionValid(session))
            {
                // Se agrega la Session a la DB: _sessionManagement.InsertSession(session);
                _sessionManagement.InsertSession(session);
            }
            return session.Token;
        }

        private bool IsSessionValid(Session session)
        {
            if (session == null)
            {
                throw new SessionException("Sesión inválida.");
            }
            if (session.IdSession == null || session.IdSession.Length < 1)
            {
                throw new SessionException("Identificador inválido.");
            }
            if (IsIdSessionRegistered(session.IdSession))
            {
                throw new SessionException("Ya existe una sesión activa con el mismo identificador");
            }
            if (session.UserName == null || session.UserName.Length == 0 || session.UserName.Length > 20)
            {
                throw new SessionException("Nombre de usuario inválido.");
            }
            if (!IsUserNameOk(session.UserName))
            {
                throw new SessionException("El usuario no existe.");
            }
            if (IsNameLogged(session))
            {
                throw new SessionException("El usuario ya fue logueado.");
            }
            if (session.Token == null || session.Token.Length < 1)
            {
                throw new SessionException("Token inválido.");
            }
            return true;
        }

        public bool IsIdSessionRegistered(string idSession)
        {
            return _sessionManagement.IsIdSessionRegistered(idSession);
        }

        private bool IsUserNameOk(string userName)
        {
            // Se chequea que el usuario exista en la DB.
            return _sessionManagement.IsUserNameOk(userName);
        }

        private bool IsNameLogged(Session session)
        {
            return _sessionManagement.IsNameLogged(session);
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
                throw new NotFoundException();
            } 
            else
            {
                string userName = session.UserName;
                User user = _userManagement.GetUserByName(userName);
                if(user == null)
                {
                    throw new NotFoundException();
                }
                return user.Role;
            }
        }
    }
}
