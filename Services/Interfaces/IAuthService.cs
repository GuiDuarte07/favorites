using favorites.Models.Entities;

namespace favorites.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> AuthenticateUserAsync(User user);
    }
}
