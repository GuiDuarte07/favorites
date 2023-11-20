﻿namespace favorites.Models
{
    public class ContentFavorite : Favorite
    {
        public string ContentType { get; set; } = string.Empty;
        public bool Complete { get; set; } = false;
        public TimeSpan TimeSpent;
    }
}