using favorites.Models.DTOs.User;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserInfoDTO?> GetUserByIdAsync(long id);
        Task<UserInfoDTO> CreateUserAsync(CreateUserDO userDTO);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
