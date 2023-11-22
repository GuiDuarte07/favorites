using favorites.Models.DTOs.User;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserInfoDTO?> GetUserById(long id);
        Task<UserInfoDTO> CreateUser(CreateUserDO userDTO);
    }
}
