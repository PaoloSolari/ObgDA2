using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Repositories;
using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Test
{
    [TestClass]
    public class SessionManagementTest
    {
        private Session session;
        private List<Session> sessions;

        [TestInitialize]
        public void InitTest()
        {
            session = new Session("ASHUDS", "Lucas", "GUSQ22");

            sessions = new List<Session>() { session };
        }

        [TestMethod]
        public void InsertSessionOk()
        {
            ObgContext context = CreateContext();
            ISessionManagement sessionManagement = new SessionManagement(context);

            sessionManagement.InsertSession(session);

            Session sessionInDatabase = context.Sessions.Where<Session>(d => d.IdSession.Equals(session.IdSession)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(sessionInDatabase);
            Assert.AreEqual(sessionInDatabase.IdSession, session.IdSession);
        }

        [TestMethod]
        public void GetSessionsOk()
        {
            ISessionManagement sessionManagement = CreateSessionManagement();
            IEnumerable<Session> sessionsInDatabase = sessionManagement.GetSessions();

            Assert.AreEqual(sessionsInDatabase.ToList().Count, sessions.Count);
            Assert.AreEqual(sessionsInDatabase.ToList()[0].IdSession, sessions[0].IdSession);

        }

        [TestMethod]
        public void GetSessionByIdOk()
        {
            ObgContext context = CreateContext();
            ISessionManagement sessionManagement = new SessionManagement(context);

            context.Sessions.Add(session);
            context.SaveChanges();

            Session sessionInDatabase = sessionManagement.GetSessionById(session.IdSession);

            Assert.IsNotNull(sessionInDatabase);
            Assert.AreEqual(sessionInDatabase.IdSession, session.IdSession);
        }

        [TestMethod]
        public void UpdateSessionOk()
        {
            ObgContext context = CreateContext();
            ISessionManagement sessionManagement = new SessionManagement(context);

            context.Sessions.Add(session);
            context.SaveChanges();
            session.UserName = "Juan";
            sessionManagement.UpdateSession(session);

            Session sessionInDatabase = context.Sessions.Where<Session>(d => d.IdSession.Equals(session.IdSession)).AsNoTracking().FirstOrDefault();

            Assert.IsNotNull(sessionInDatabase);
            Assert.AreEqual(sessionInDatabase.UserName, session.UserName);
        }

        [TestMethod]
        public void DeleteSessionOk()
        {
            ObgContext context = CreateContext();
            ISessionManagement sessionManagement = new SessionManagement(context);

            context.Sessions.Add(session);
            context.SaveChanges();

            sessionManagement.DeleteSession(session);

            Session sessionInDatabase = context.Sessions.Where<Session>(d => d.IdSession.Equals(session.IdSession)).AsNoTracking().FirstOrDefault();

            Assert.IsNull(sessionInDatabase);
        }

        private ObgContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ObgContext>()
                .UseInMemoryDatabase("ObgDA2")
                .Options;

            var context = new ObgContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        private ISessionManagement CreateSessionManagement()
        {
            var context = CreateContext();

            context.Sessions.Add(session);
            context.SaveChanges();

            ISessionManagement sessionManagement = new SessionManagement(context);
            return sessionManagement;
        }
    }
}
