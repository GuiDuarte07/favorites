using favorites.Models.DTOs.User;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticate([FromBody] LoginUserDTO userCredentials)
        {
            var user = await _authRepository.ValidateCredentialsAsync(userCredentials.Email, userCredentials.Password);

            if (user == null)
            {
                return NotFound(new { message = "Email ou senha inválidos" });
            }

            var token = _tokenService.GenerateToken(user);

            return new
            {
                user = new UserInfoDTO() { Email = user.Email, Id = user.Id, Name = user.Name, Folders = user.Folders },
                token
            };
        }
    }
}
