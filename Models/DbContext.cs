using favorites.Models.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new FolderMap());
            modelBuilder.ApplyConfiguration(new FavoriteMap());
        }
    }
}
