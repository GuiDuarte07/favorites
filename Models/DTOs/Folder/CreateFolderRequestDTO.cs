using System.ComponentModel.DataAnnotations;

namespace favorites.Models.DTOs.Folder
{
    public class CreateFolderRequestDTO
    {
        public long? ParentFolderId { get; set; }

        [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
        public string Name { get; set; }
    }
}
