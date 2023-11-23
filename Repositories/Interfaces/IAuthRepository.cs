using favorites.Models.Entities;
using favorites.Services;

namespace favorites.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public Task<User?> ValidateCredentialsAsync(string email, string password);
    }
}