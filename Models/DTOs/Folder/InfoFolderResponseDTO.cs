namespace favorites.Models.DTOs.Folder
{
    public class InfoFolderResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public List<SubFolderResponseDTO> SubFolders { get; set; }
    }
}