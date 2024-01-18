using favorites.Models.DTOs.Folder;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;
        private readonly ITokenService _tokenService;
        public FolderController(IFolderRepository folderRepository, ITokenService tokenService) 
        {
            _folderRepository = folderRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Folder>> CreateFolder([FromBody] CreateFolderRequestDTO folder)
        {
            var token = HttpContext.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return Unauthorized();
            }

            if (folder.Name.ToLower() == "Favoritos".ToLower())
            {
                return BadRequest("'Favoritos' não é um nome de pasta válido");
            }

            long userId = _tokenService.GetUserIdFromToken(token);

            var createdFolder = await _folderRepository.CreateFolderAsync(folder, userId);

            return createdFolder;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<InfoFolderResponseDTO?> GetFolder(long id)
        {
            var folder = _folderRepository.GetFolder(id);

            if (folder == null)
            {
                return NotFound();
            }

            var infoFolder = new InfoFolderResponseDTO() 
            {
                Id = folder.Id, 
                Name = folder.Name,
                UserId = folder.User?.Id ?? -1,
                SubFolders = folder.SubFolders?.Select(sb => new SubFolderResponseDTO() { Id = sb.Id, Name = sb.Name}).ToList() ?? new List<SubFolderResponseDTO>()
            };

            return infoFolder;
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateFolder([FromBody] UpdateFolderRequestDTO folderUpdateInfo) 
        {
            var updatedFolder = await _folderRepository.UpdateFolderAsync(folderUpdateInfo);

            if (updatedFolder == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
