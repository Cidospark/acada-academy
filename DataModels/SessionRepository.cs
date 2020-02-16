using AcadaAcademy.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AcadaContext context;

        public SessionRepository(AcadaContext context)
        {
            this.context = context;
        }
        public Session AddSession(Session session)
        {
            context.Sessions.Add(session);
            context.SaveChanges();
            return session;
        }

        public Session DeleteSession(int id)
        {
            var session = context.Sessions.Find(id);
            if(session != null)
            {
                context.Sessions.Remove(session);
                context.SaveChanges();
            }
            return session;
        }

        public IEnumerable<Session> fetchAllSession()
        {
            return context.Sessions;
        }

        public Session fetchSession(int id)
        {
            return context.Sessions.Find(id);
        }

        public Session UpdateSession(Session sessionChanges)
        {
            //Attach sessionChanges to context;
            var session = context.Sessions.Attach(sessionChanges);
            // mark as context state as modified
            session.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // save changes
            context.SaveChanges();
            return sessionChanges;
        }
    }
}
