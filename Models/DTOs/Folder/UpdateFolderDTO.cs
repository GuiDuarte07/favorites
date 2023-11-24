namespace favorites.Models.DTOs.Folder
{
    public class UpdateFolderDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsFavoriteFolder { get; set; }

    }
}
