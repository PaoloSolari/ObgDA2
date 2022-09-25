using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class SessionService
    {
        private readonly ISessionManagement _sessionManagement;

        public SessionService(ISessionManagement sessionManagement)
        {
            _sessionManagement = sessionManagement;
        }

        public void InsertSession(Session session)
        {
            if (IsSessionValid(session))
            {
                // Se agrega la Session a la DB: _sessionManagement.InsertSession(session);
                FakeDB.Sessions.Add(session);
            }
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
            if (IsNameLogged(session.UserName))
            {
                throw new SessionException("El usuario ya fue logueado.");
            }
            if(session.Token == null || session.Token.Length < 1)
            {
                throw new SessionException("Token inválido.");
            }
            return true;
        }

        public bool IsIdSessionRegistered(string idSession)
        {
            foreach (Session session in FakeDB.Sessions)
            {
                if (session.IdSession.Equals(idSession))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsUserNameOk(string userName)
        {
            // Se chequea que el usuario exista en la DB.
            foreach (User user in FakeDB.Users)
            {
                if (userName.Equals(user.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsNameLogged(string name)
        {
            foreach (Session session in FakeDB.Sessions)
            {
                if (name.Equals(session.UserName))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
