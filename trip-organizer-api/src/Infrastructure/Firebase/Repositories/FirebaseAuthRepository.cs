using FirebaseAdmin.Auth;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Infrastructure.Firebase.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        public async Task<UserRecord> VerifyIdTokenAsync(string idToken)
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            var uid = decodedToken.Uid;
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            return userRecord;
        }

    }
}
