using obg.Domain.Entities;

namespace obg.BusinessLogic.Interface
{
    public interface ISessionService
    {
        string InsertSession(Session session);
        bool IsTokenValid(string token);
    }
}