using FirebaseAdmin.Auth;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Infrastructure.Firebase.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        private static readonly FirebaseAuth _auth = FirebaseInstance.GetAuth();
        public async Task<UserRecord> VerifyIdTokenAsync(string idToken)
        {
            var decodedToken = await _auth.VerifyIdTokenAsync(idToken);
            var uid = decodedToken.Uid;
            var userRecord = await _auth.GetUserAsync(uid);
            return userRecord;
        }

    }
}
