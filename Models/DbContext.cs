using Microsoft.EntityFrameworkCore;

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

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasDiscriminator<string>("FavoriteType")
                .HasValue<Favorite>("Favorite")
                .HasValue<ContentFavorite>("ContentFavorite");
        }*/
    }
}
