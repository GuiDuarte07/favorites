using favorites.Models.DTOs.Folder;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        public Task<Folder> CreateFolderAsync(CreateFolderRequestDTO folder, long UserId);

        public Task<Folder?> UpdateFolderAsync(UpdateFolderRequestDTO folderUpdateInfo);

        public Folder? GetFolder(long id);
    }
}
