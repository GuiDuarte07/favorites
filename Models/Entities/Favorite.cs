using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace favorites.Models.Entities
{
    public class Favorite
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? FaviconUrl { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool Fixed { get; set; } = false;
        public string ContentType { get; set; } = string.Empty;
        public bool Complete { get; set; } = false;
        public long? TimeSpentTicks { get; set; } // Armazenar os ticks do TimeSpan

        public long FolderId { get; set; }
        public Folder? Folder { get; set; }
    }

    public class FavoriteMap : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name).IsRequired().HasMaxLength(255).HasColumnType("varchar(255)");
            builder.Property(f => f.Url).IsRequired().HasMaxLength(500).HasColumnType("varchar(500)");
            builder.Property(f => f.FaviconUrl).IsRequired(false).HasMaxLength(255).HasColumnType("varchar(255)");
            builder.Property(f => f.Notes).HasMaxLength(500).HasColumnType("varchar(500)");
            builder.Property(f => f.Fixed).IsRequired().HasDefaultValue(false);
            builder.Property(f => f.ContentType).IsRequired().HasMaxLength(50);
            builder.Property(f => f.Complete).IsRequired().HasDefaultValue(false);
            builder.Property(f => f.TimeSpentTicks).IsRequired(false).HasColumnName("TimeSpent").HasColumnType("bigint");

            builder.HasOne(f => f.Folder) // Um favorite pertence a um folder
                .WithMany(f => f.Favorites) // Um folder pode ter muitos favorites
                .HasForeignKey(f => f.FolderId) // Chave estrangeira para Folder
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Define a ação de exclusão conforme necessário
        }
    }
}
