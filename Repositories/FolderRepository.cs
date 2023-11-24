using favorites.Models;
using favorites.Models.DTOs.Folder;
using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace favorites.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly FavoriteContext _context;

        public FolderRepository(FavoriteContext context)
        {
            _context = context;
        }
        public async Task<Folder> CreateFolderAsync(CreateFolderDTO folder, long userId)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new NullReferenceException("O usuário não pôde ser encontrado.");

            var folderEntity = new Folder() { Name = folder.Name, ParentFolderId = folder.ParentFolderId, UserId = userId, User = user };

            var createdFolder = _context.Folders.Add(folderEntity);
            await _context.SaveChangesAsync();

            return createdFolder.Entity;
        }

        public InfoFolderDTO? GetFolder(long id)
        {
            var folder = _context.Folders
                .Where(f => f.Id == id)
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    User = new
                    {
                        f.User.Id
                    },
                    SubFolders = f.SubFolders.Select(sf => new
                    {
                        sf.Id,
                        sf.Name
                    }).ToList()
                }).FirstOrDefault();

            var infoFolder = new InfoFolderDTO
            {
                Id = id,
                Name = folder.Name,
                UserId = folder.User.Id,
                SubFolders = folder.SubFolders.Select(sf => new SubFolderDTO
                {
                    Id = sf.Id,
                    Name = sf.Name
                }).ToList()
            };

            return infoFolder;

            /*,
            Favorites = f.Favorites.Select(fav => new
            {
                fav.Id,
                fav.Name,
                fav.Fixed,
                fav.Url
            })
            */
        }

        public async Task<Folder?> UpdateFolderAsync(UpdateFolderDTO folderUpdateInfo)
        {
            var folderToUpdate = _context.Folders.FirstOrDefault(f => f.Id == folderUpdateInfo.Id);

            if (folderToUpdate == null)
            {
                return null;
            }

            folderToUpdate.Name = folderUpdateInfo.Name;
            folderToUpdate.IsFavoriteFolder = folderUpdateInfo.IsFavoriteFolder;

            _context.Folders.Update(folderToUpdate);
            await _context.SaveChangesAsync();

            return folderToUpdate;
        }
    }
}
