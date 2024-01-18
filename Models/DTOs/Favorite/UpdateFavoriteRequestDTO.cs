using System.ComponentModel.DataAnnotations;

namespace favorites.Models.DTOs.Favorite
{
    public class UpdateFavoriteRequestDTO
    {
        [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
        public long Id {  get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? FaviconUrl { get; set; }
        public string? Notes { get; set; }
        public bool? Fixed { get; set; }
        public string? ContentType { get; set; }
        public bool? Complete { get; set; }
        public long? TimeSpentTicks { get; set; }
    }
}
