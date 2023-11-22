using favorites.Models;
using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services;
using Microsoft.EntityFrameworkCore;

namespace favorites.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FavoriteContext _context;
        private readonly IHashService _hashService;

        public UserRepository(FavoriteContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<UserInfoDTO> CreateUser(CreateUserDO userDTO)
        {
            // Fazendo o hash da senha
            string hashedPassword = _hashService.HashPassword(userDTO.Password);

            // Instânciando uma nova classe User com os dados do userDTO
            var user = new User { Name = userDTO.Name, Email = userDTO.Email, Password = hashedPassword };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserInfoDTO { Email = user.Email, Name = user.Name, Id = user.Id };
        }

        public async Task<UserInfoDTO?> GetUserById(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            var userInfo = new UserInfoDTO() { Email = user.Email, Folders = user.Folders, Id = user.Id, Name = user.Name };

            return userInfo;
        }
    }
}
