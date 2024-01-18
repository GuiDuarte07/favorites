using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        public Task<List<Favorite>?> GetFavoritesAsync(long? folderId, long userId);
        public Task<Favorite> CreateFavoriteAsync(CreateFavoriteRequestDTO createFavoriteDetails);
        public Task<Favorite?> UpdateFavoriteAsync(UpdateFavoriteRequestDTO updateFavoriteDetails);
    }
}
