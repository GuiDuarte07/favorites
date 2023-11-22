using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace favorites.Models.Entities
{
    public class ContentFavorite : Favorite
    {
        public string ContentType { get; set; } = string.Empty;
        public bool Complete { get; set; } = false;

        public long? TimeSpentTicks { get; set; } // Armazenar os ticks do TimeSpan
    }

    public class ContentFavoriteMap : IEntityTypeConfiguration<ContentFavorite>
    {
        public void Configure(EntityTypeBuilder<ContentFavorite> builder)
        {
            // Mapeamento das propriedades específicas de ContentFavorite
            builder.Property(cf => cf.ContentType).IsRequired().HasMaxLength(50);
            builder.Property(cf => cf.Complete).IsRequired().HasDefaultValue(false);
            builder.Property(cf => cf.TimeSpentTicks).IsRequired(false).HasColumnName("TimeSpent").HasColumnType("bigint");

            // Configuração da relação de herança (TPT - Table Per Type)
            //builder.HasBaseType<Favorite>();
        }
    }
}
