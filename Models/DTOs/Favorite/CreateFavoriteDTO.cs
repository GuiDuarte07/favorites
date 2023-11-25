namespace favorites.Models.DTOs.Favorite
{
    public class CreateFavoriteDTO
    {
        public long FolderId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? FaviconUrl { get; set; }
        public string? Notes { get; set; }
        public bool Fixed { get; set; } = false;
        public string ContentType { get; set; }
        public bool? Complete { get; set; } = false;
        public long? TimeSpentTicks { get; set; }
    }
}
