namespace favorites.Models.Entities
{
    public class Favorite
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string FaviconUrl { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool Fixed { get; set; } = false;

        public long FolderId { get; set; }
        public Folder? Folder { get; set; }
    }
}
