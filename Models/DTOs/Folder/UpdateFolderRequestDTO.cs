using System.ComponentModel.DataAnnotations;

namespace favorites.Models.DTOs.Folder
{
    public class UpdateFolderRequestDTO
    {
        [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool? IsFavoriteFolder { get; set; }

    }
}
