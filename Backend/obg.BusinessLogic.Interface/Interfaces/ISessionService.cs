using obg.Domain.Entities;
using obg.Domain.Enums;

namespace obg.BusinessLogic.Interface
{
    public interface ISessionService
    {
        Session GetSessionByToken(string token);
        Session GetSessionByName(string userName);
        string InsertSession(Session session, string password);
        bool IsTokenValid(string token);
        RoleUser GetUserRole(string token);
    }
}