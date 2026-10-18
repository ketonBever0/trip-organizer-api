using FirebaseAdmin.Auth;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Application.Interfaces
{
    public interface IAuthRepository
    {
        public Task<UserRecord> VerifyIdTokenAsync(string idToken);
        public Task<string> LoginWithEmailAndPasswordAsync(string email, string password);
        public Task SignUpWithEmailAndPasswordAsync(string email, string password);
    }
}
