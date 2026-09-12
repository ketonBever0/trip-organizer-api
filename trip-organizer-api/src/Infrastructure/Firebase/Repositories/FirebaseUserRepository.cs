using FirebaseAdmin.Auth;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Infrastructure.Firebase.Repositories
{
    public class FirebaseUserRepository : IUserRepository
    {
        private FirebaseAuth Auth => FirebaseAuth.DefaultInstance;
        public async Task<List<UserRecord>> ListUsersAsync()
        {
            var result = new List<UserRecord>();
            var paged = Auth.ListUsersAsync(null);

            await foreach (var user in paged)
            {
                result.Add(user);
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
