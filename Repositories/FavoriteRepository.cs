using favorites.Models;
using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using static NuGet.Packaging.PackagingConstants;

namespace favorites.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly FavoriteContext _context;

        public FavoriteRepository(FavoriteContext context)
        {
            _context = context;
        }
        public async Task<Favorite> CreateFavoriteAsync(CreateFavoriteDTO createFavoriteDetails)
        {
            var newFavorite = new Favorite()
            {
                FolderId = createFavoriteDetails.FolderId,
                Name = createFavoriteDetails.Name,
                Url = createFavoriteDetails.Url,
                ContentType = createFavoriteDetails.ContentType,
                Complete = createFavoriteDetails.Complete,
                FaviconUrl = createFavoriteDetails.FaviconUrl,
                Fixed = createFavoriteDetails.Fixed,
                Notes = createFavoriteDetails.Notes,
                TimeSpentTicks = createFavoriteDetails.TimeSpentTicks,
            };

            var favorite = await _context.Favorites.AddAsync(newFavorite);
            await _context.SaveChangesAsync();

            return favorite.Entity;
        }

        public async Task<List<Favorite>?> GetFavoritesAsync(long? folderId, long userId)
        {
            var favorites = _context.Favorites
                .Where(f => f.FolderId == folderId && f.Folder.UserId == userId)
                .ToList();

            return favorites;
        }

        public async Task<Favorite?> UpdateFavoriteAsync(UpdateFavoriteDTO updateFavoriteDetails)
        {
            var favorite = await _context.Favorites.FindAsync(updateFavoriteDetails.Id) ?? throw new NullReferenceException();

            if (updateFavoriteDetails.Name != null)
                favorite.Name = updateFavoriteDetails.Name;

            if (updateFavoriteDetails.Url != null)
                favorite.Url = updateFavoriteDetails.Url;

            if (updateFavoriteDetails.ContentType != null)
                favorite.ContentType = updateFavoriteDetails.ContentType;

            if (updateFavoriteDetails.Complete.HasValue)
                favorite.Complete = updateFavoriteDetails.Complete.Value;

            if (updateFavoriteDetails.FaviconUrl != null)
                favorite.FaviconUrl = updateFavoriteDetails.FaviconUrl;

            if (updateFavoriteDetails.Fixed.HasValue)
                favorite.Fixed = updateFavoriteDetails.Fixed.Value;

            if (updateFavoriteDetails.Notes != null)
                favorite.Notes = updateFavoriteDetails.Notes;

            if (updateFavoriteDetails.TimeSpentTicks.HasValue)
                favorite.TimeSpentTicks = updateFavoriteDetails.TimeSpentTicks.Value;

            var updatedFavorite = _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();

            return updatedFavorite.Entity;
        }
    }
}
