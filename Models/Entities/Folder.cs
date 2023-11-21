namespace favorites.Models.Entities
{
    public class Folder
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFavoriteFolder { get; set; }
        public long? Parent_folder { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        public List<Favorite>? Favorites { get; set; }
        public List<Folder>? Folders { get; set; }
    }
}
