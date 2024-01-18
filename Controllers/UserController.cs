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
        private readonly IFolderRepository _folderRepository;
        public UserController(IUserRepository userRepository, IFolderRepository folderRepository) 
        {
            _userRepository = userRepository;
            _folderRepository = folderRepository;
        }

        [HttpPost]
        public async Task<ActionResult<InfoUserResponseDTO>> CreateUser(CreateUserRequestDTO userDTO)
        {
            // Não sei se essa verificação é necessária
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.CreateUserAsync(userDTO);

            await _folderRepository.CreateFolderAsync(
                new CreateFolderRequestDTO { Name = "Favoritos", ParentFolderId = null },
                user.Id
                );

            var userInfo = new InfoUserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Folders = user.Folders?.Select(folder => new InfoFolderResponseDTO
                {
                    Id = folder.Id,
                    Name = folder.Name,
                    UserId = user.Id,
                    SubFolders = folder.SubFolders?.Select(subFolder => new SubFolderResponseDTO
                    {
                        Id = subFolder.Id,
                        Name = subFolder.Name
                    }).ToList() ?? new List<SubFolderResponseDTO>()
                }).ToList() ?? new List<InfoFolderResponseDTO>()
            };

            return CreatedAtAction(nameof(GetUser), new { id = userInfo.Id }, userInfo);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoUserResponseDTO>> GetUser(long id)
        {
            
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new InfoUserResponseDTO() 
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Folders = user.Folders?.Select(folder => new InfoFolderResponseDTO
                {
                    Id = folder.Id,
                    Name = folder.Name,
                    UserId = user.Id,
                    SubFolders = folder.SubFolders?.Select(subFolder => new SubFolderResponseDTO
                    {
                        Id = subFolder.Id,
                        Name = subFolder.Name
                    }).ToList() ?? new List<SubFolderResponseDTO>()
                }).ToList() ?? new List<InfoFolderResponseDTO>()
            };

            return userInfo;
        }
    }
}
