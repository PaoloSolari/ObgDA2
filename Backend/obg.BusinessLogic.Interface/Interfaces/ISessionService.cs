using obg.Domain.Entities;
using obg.Domain.Enums;

namespace obg.BusinessLogic.Interface
{
    public interface ISessionService
    {
        string InsertSession(Session session, string password);
        bool IsTokenValid(string token);
        RoleUser GetUserRole(string token);
    }
}