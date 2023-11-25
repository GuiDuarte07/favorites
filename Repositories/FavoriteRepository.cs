using favorites.Models;
using favorites.Models.DTOs.Favorite;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using static Azure.Core.HttpHeader;
using System.Numerics;
using System.Security.Policy;

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

        public async Task<Favorite?> GetFavoriteAsync(long id)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            return favorite;
        }

        public async Task<Favorite?> UpdateFavoriteAsync(UpdateFavoriteDTO updateFavoriteDetails)
        {
            var favorite = await _context.Favorites.FindAsync(updateFavoriteDetails.Id) ?? throw new NullReferenceException();

            favorite.Name = updateFavoriteDetails.Name;
            favorite.Url = updateFavoriteDetails.Url;
            favorite.ContentType = updateFavoriteDetails.ContentType;
            favorite.Complete = updateFavoriteDetails.Complete;
            favorite.FaviconUrl = updateFavoriteDetails.FaviconUrl;
            favorite.Fixed = updateFavoriteDetails.Fixed ?? false;
            favorite.Notes = updateFavoriteDetails.Notes;
            favorite.TimeSpentTicks = updateFavoriteDetails.TimeSpentTicks;

            var updatedFavorite = _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();

            return updatedFavorite.Entity;
        }
    }
}
