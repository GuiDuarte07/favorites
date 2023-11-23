using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;

namespace favorites.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public AuthRepository(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            // Acesso ao UserRepository para validação das credenciais
            var user = await _userRepository.GetUserByEmailAsync(email);

            // Verificar se o usuário existe e se a senha está correta
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            var hashedPassword = _hashService.HashPassword(password);
            return passwordHash == hashedPassword;
        }
    }

}
