using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface ISessionManagement
    {
        void InsertSession(Session session);
        IEnumerable<Session> GetSessions();
        Session GetSessionById(string id);
        Session GetSessionByToken(string token);
        void UpdateSession(Session session);
        void DeleteSession(Session session);
        bool IsIdSessionRegistered(string idSession);
        bool IsNameLogged(Session session);
        bool IsTokenValid(string token);
    }
}
