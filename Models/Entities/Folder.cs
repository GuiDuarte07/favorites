using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;

namespace favorites.Models.Entities
{
    public class Folder
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFavoriteFolder { get; set; }

        public long? ParentFolderId { get; set; }
        public Folder? ParentFolder { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        public List<Favorite>? Favorites { get; set; }
        public List<Folder>? SubFolders { get; set; }
    }

    public class FolderMap : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.ToTable("Folders");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name).IsRequired().HasMaxLength(40).HasColumnType("varchar(40)");

            builder.Property(f => f.IsFavoriteFolder).IsRequired().HasDefaultValue(false);

            builder.HasOne(f => f.User) // Tem somente um user
                .WithMany(f => f.Folders) // Um user tem vários folders
                .HasForeignKey(f => f.UserId) // Chave estrangeira
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.ParentFolder)
                .WithMany(f => f.SubFolders) // Propriedade de navegação para os subfolders
                .HasForeignKey(f => f.ParentFolderId)
                .IsRequired(false) // Tem um ou nenhum
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Favorites) // Um folder tem muitos favorites
            .WithOne() // O Favorite não possui uma referência direta para Folder
            .IsRequired(false) // Pode ter nenhum favorite
            .HasForeignKey(f => f.FolderId) // Chave estrangeira em Favorite
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}