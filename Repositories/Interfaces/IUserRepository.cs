using favorites.Models.DTOs.User;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(long id);
        Task<User> CreateUserAsync(CreateUserDO userDTO);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
