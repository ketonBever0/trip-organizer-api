using trip_organizer_api.src.Application.Interfaces;

namespace trip_organizer_api.src.Application
{
    public interface IDBContext
    {
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
    }
}
