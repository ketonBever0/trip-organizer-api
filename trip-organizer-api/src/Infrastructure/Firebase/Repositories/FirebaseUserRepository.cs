using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Infrastructure.Firebase.Repositories
{
    public class FirebaseUserRepository : IUserRepository
    {
        private static readonly FirebaseAuth _auth = FirebaseInstance.GetAuth();
        private static readonly FirestoreDb _db = FirebaseInstance.GetFirestore();
        
        public async Task<List<User>> ListAuthUsersAsync()
        {
            var users = _auth.ListUsersAsync(null);
            var result = new List<User>();
            await foreach (var user in users)
            {
                result.Add(new User
                {
                    ID = user.Uid,
                    Firstname = user.DisplayName?.Split(' ').FirstOrDefault() ?? string.Empty,
                    Lastname = user.DisplayName?.Split(' ').Skip(1).FirstOrDefault() ?? string.Empty,
                    Nick = user.DisplayName?.Split(' ').Skip(2).FirstOrDefault()
                });
            }
            return result;
        }

        public async Task<List<User>> ListUsersAsync()
        {
            var result = new List<User>();
            var paged = _auth.ListUsersAsync(null);

            await foreach (var user in paged)
            {
                var snap = await _db.Collection("users").Document(user.Uid).GetSnapshotAsync();
                if (!snap.Exists)
                {
                    continue;
                }
                result.Add(new User
                {
                    ID = user.Uid,
                    Firstname = snap.GetValue<string>("firstname"),
                    Lastname = snap.GetValue<string>("lastname"),
                    Nick = snap.GetValue<string>("nick")
                });
            }

            return result;
        }

        public Task DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
