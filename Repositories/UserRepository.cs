using favorites.Models;
using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
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

        public async Task<User> CreateUserAsync(CreateUserRequestDTO userDTO)
        {
            // Fazendo o hash da senha
            string hashedPassword = _hashService.HashPassword(userDTO.Password);

            // Instânciando uma nova classe User com os dados do userDTO
            var user = new User { Name = userDTO.Name, Email = userDTO.Email, Password = hashedPassword };


            var createdUser = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return createdUser.Entity;
        }

        public async Task<User?> GetUserByIdAsync(long id)
        {
            var userInfo = await _context.Users.Include(u => u.Folders).FirstOrDefaultAsync(u => u.Id == id);

            if (userInfo == null)
            {
                return null;
            }

            return userInfo;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

    }
}
