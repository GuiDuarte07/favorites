using favorites.Models.DTOs.Folder;
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
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticate([FromBody] LoginUserRequestDTO userCredentials)
        {
            var user = await _authRepository.ValidateCredentialsAsync(userCredentials.Email, userCredentials.Password);

            if (user == null)
            {
                return NotFound(new { message = "Email ou senha inválidos" });
            }

            var token = _tokenService.GenerateToken(user);

            return new
            {
                user = new InfoUserResponseDTO()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    Folders = user.Folders?
                        .Select(f => new InfoFolderResponseDTO()
                        {
                            Id = f.Id,
                            Name = f.Name,
                            UserId = user.Id,
                            SubFolders = f.SubFolders?
                                .Select(sb => new SubFolderResponseDTO()
                                {
                                    Id = sb.Id,
                                    Name = sb.Name
                                }).ToList() ?? new List<SubFolderResponseDTO>(),
                        }).ToList() ?? new List<InfoFolderResponseDTO>(),
                },
                token
            };
        }
    }
}
