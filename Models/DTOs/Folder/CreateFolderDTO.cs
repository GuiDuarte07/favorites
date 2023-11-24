namespace favorites.Models.DTOs.Folder
{
    public class CreateFolderDTO
    {
        public long? ParentFolderId { get; set; }
        public string Name { get; set; }
    }
}
