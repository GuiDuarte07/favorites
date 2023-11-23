using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
using System.Security.Authentication;

namespace favorites.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            // Validação das credenciais
            var user = await _authRepository.ValidateCredentialsAsync(email, password);
            if (user == null)
            {
                // Tratar caso as credenciais sejam inválidas
                throw new AuthenticationException("Credenciais inválidas");
            }

            // Geração do token JWT
            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }

}
