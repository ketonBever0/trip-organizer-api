using FirebaseAdmin.Auth;
using trip_organizer_api.src.Domain.Entities;

namespace trip_organizer_api.src.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> ListUsersAsync();
        public Task<List<User>> ListAuthUsersAsync();
        public Task<User> CreateUserAsync();
        public Task DeleteUserAsync(string userId);
    }
}
