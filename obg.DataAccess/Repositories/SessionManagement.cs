using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class SessionManagement : ISessionManagement
    {
        private ObgContext ObgContext { get; set; }
        public SessionManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertSession(Session session)
        {
            ObgContext.Sessions.Add(session);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Session> GetSessions()
        {
            return ObgContext.Sessions.ToList();
        }

        public Session GetSessionById(string id)
        {
            return ObgContext.Sessions.Where<Session>(d => d.IdSession == id).AsNoTracking().FirstOrDefault();
        }

        public Session GetSessionByToken(string token)
        {
            return ObgContext.Sessions.Where<Session>(s => s.Token.Equals(token)).AsNoTracking().FirstOrDefault();
        }

        public void UpdateSession(Session session)
        {
            ObgContext.Sessions.Attach(session);
            ObgContext.Entry(session).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteSession(Session session)
        {
            ObgContext.Sessions.Remove(session);
            ObgContext.SaveChanges();
        }

        public bool IsIdSessionRegistered(string idSession)
        {
            Session session = ObgContext.Sessions.Where<Session>(s => s.IdSession.Equals(idSession)).AsNoTracking().FirstOrDefault();
            if(session != null)
            {
                return true;
            }
            return false;
        }

        public bool IsNameLogged(Session session)
        {
            Session sessionDB = ObgContext.Sessions.Where<Session>(s => s.UserName.Equals(session.UserName)).AsNoTracking().FirstOrDefault();
            if(sessionDB != null)
            {
                return true;
            }
            return false;
        }

        public bool IsTokenValid(string token)
        {
            Session sessionDB = ObgContext.Sessions.Where<Session>(s => s.Token.Equals(token)).AsNoTracking().FirstOrDefault();
            if( sessionDB != null)
            {
                return true;
            }
            return false;
        }
    }
}
