using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace favorites.Models.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Folder>? Folders { get; set; }
    }

    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar");

            builder.Property(u => u.Email).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password).IsRequired().HasMaxLength(64).HasColumnType("char");
        }
    }
}
