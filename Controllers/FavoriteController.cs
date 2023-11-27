using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] CreateFavoriteDTO Createfavorite)
        {

            var createdFolder = await _favoriteRepository.CreateFavoriteAsync(Createfavorite);

            return createdFolder;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite?>> GetFavorite(long id)
        {
            var favorite = await _favoriteRepository.GetFavoriteAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
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
