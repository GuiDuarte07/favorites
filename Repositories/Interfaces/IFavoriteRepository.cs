using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        public Task<List<Favorite>?> GetFavoritesAsync(long? folderId, long userId);
        public Task<Favorite> CreateFavoriteAsync(CreateFavoriteDTO createFavoriteDetails);
        public Task<Favorite?> UpdateFavoriteAsync(UpdateFavoriteDTO updateFavoriteDetails);
    }
}
