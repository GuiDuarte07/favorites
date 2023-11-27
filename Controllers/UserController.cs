using favorites.Models.DTOs.Folder;
using favorites.Models.DTOs.User;
using favorites.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<InfoUserDTO>> CreateUser(CreateUserDO userDTO)
        {
            // Não sei se essa verificação é necessária
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInfoDTO = await _userRepository.CreateUserAsync(userDTO);

            return CreatedAtAction(nameof(GetUser), new { id = userInfoDTO.Id }, userInfoDTO);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoUserDTO>> GetUser(long id)
        {
            
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new InfoUserDTO() 
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Folders = user.Folders?.Select(folder => new InfoFolderDTO
                {
                    Id = folder.Id,
                    Name = folder.Name,
                    UserId = user.Id,
                    SubFolders = folder.SubFolders?.Select(subFolder => new SubFolderDTO
                    {
                        Id = subFolder.Id,
                        Name = subFolder.Name
                    }).ToList() ?? new List<SubFolderDTO>()
                }).ToList() ?? new List<InfoFolderDTO>()
            };

            return userInfo;
        }
    }
}
