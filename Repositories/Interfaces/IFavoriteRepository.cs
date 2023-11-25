using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        public Task<Favorite?> GetFavoriteAsync(long id);
        public Task<Favorite> CreateFavoriteAsync(CreateFavoriteDTO createFavoriteDetails);
        public Task<Favorite?> UpdateFavoriteAsync(UpdateFavoriteDTO updateFavoriteDetails);
    }
}
