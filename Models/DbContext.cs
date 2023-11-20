using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace favorites.Models
{
    public class FavoriteContext : DbContext
    {
        public FavoriteContext(DbContextOptions<FavoriteContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ContentFavorite> ContentFavorites { get; set; }

    }
}
