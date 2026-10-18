using Firebase.Auth;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using trip_organizer_api.src.Application.Exceptions;
using trip_organizer_api.src.Application.Interfaces;
using FirebaseAuthException = Firebase.Auth.FirebaseAuthException;

namespace trip_organizer_api.src.Infrastructure.Firebase.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        private static readonly FirebaseAuth _auth = FirebaseInstance.GetAuth();
        private static readonly FirebaseAuthClient _authClient = FirebaseInstance.GetAuthClient();
        private static readonly FirestoreDb _db = FirebaseInstance.GetFirestore();
        
        public async Task<UserRecord> VerifyIdTokenAsync(string idToken)
        {
            var decodedToken = await _auth.VerifyIdTokenAsync(idToken);
            var uid = decodedToken.Uid;
            var userRecord = await _auth.GetUserAsync(uid);
            return userRecord;
        }

        /// <exception cref="Exception">
        /// Invalid credentials
        /// </exception>
        public async Task<string> LoginWithEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                var response = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var idToken = await response.User.GetIdTokenAsync();
                return idToken;

            }
            catch (FirebaseAuthException)
            {
                throw new BadCredentialsException("Invalid credentials.");
            }
        }

        public async Task SignUpWithEmailAndPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
