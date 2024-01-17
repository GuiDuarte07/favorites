using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository; 
            private readonly ITokenService _tokenService;
        public FavoriteController(IFavoriteRepository favoriteRepository, ITokenService tokenService)
        {
            _favoriteRepository = favoriteRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] CreateFavoriteDTO Createfavorite)
        {

            var createdFolder = await _favoriteRepository.CreateFavoriteAsync(Createfavorite);

            return createdFolder;
        }

        [Authorize]
        [HttpGet("{folderId}")]
        public async Task<ActionResult<List<Favorite>?>> GetFavorites(long folderId)
        {
            var token = HttpContext.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return Unauthorized();
            }

            long userId = _tokenService.GetUserIdFromToken(token);

            var favorites = await _favoriteRepository.GetFavoritesAsync(folderId, userId);

            if (favorites == null)
            {
                return NotFound();
            }

            return favorites;
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateFolder([FromBody] UpdateFavoriteDTO favoriteUpdateInfo)
        {
            var updatedFavorite = await _favoriteRepository.UpdateFavoriteAsync(favoriteUpdateInfo);

            if (updatedFavorite == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
