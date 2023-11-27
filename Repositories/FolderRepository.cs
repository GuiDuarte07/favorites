using favorites.Models;
using favorites.Models.DTOs.Folder;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
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

        public Folder? GetFolder(long id)
        {
            var folder = _context.Folders.Include(f => f.User).Include(f => f.SubFolders).FirstOrDefault(f => f.Id == id);

            return folder;
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
