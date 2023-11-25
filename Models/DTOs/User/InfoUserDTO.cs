using favorites.Models.DTOs.Folder;
using favorites.Models.Entities;

namespace favorites.Models.DTOs.User
{
    public class InfoUserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<InfoFolderDTO> Folders { get; set; }
    }
}
