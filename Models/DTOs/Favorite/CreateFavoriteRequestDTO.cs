using System.ComponentModel.DataAnnotations;

namespace favorites.Models.DTOs.Favorite
{
    public class CreateFavoriteRequestDTO
    {
        [Required(ErrorMessage = "O campo 'FolderId' é obrigatório.")]
        public long FolderId { get; set; }

        [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Url' é obrigatório.")]
        public string Url { get; set; }
        public string? FaviconUrl { get; set; }
        public string? Notes { get; set; }
        public bool Fixed { get; set; } = false;

        [Required(ErrorMessage = "O campo 'ContentType' é obrigatório.")]
        public string ContentType { get; set; }
        public bool? Complete { get; set; } = false;
        public long? TimeSpentTicks { get; set; }
    }
}
