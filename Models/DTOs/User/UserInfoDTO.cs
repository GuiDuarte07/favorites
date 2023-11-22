﻿using favorites.Models.Entities;

namespace favorites.Models.DTOs.User
{
    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Folder>? Folders { get; set; }
    }
}