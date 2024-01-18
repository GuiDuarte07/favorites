using favorites.Models.DTOs.User;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(long id);
        Task<User> CreateUserAsync(CreateUserRequestDTO userDTO);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
