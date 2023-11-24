namespace favorites.Models.DTOs.Folder
{
    public class InfoFolderDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public List<SubFolderDTO> SubFolders { get; set; }
    }
}