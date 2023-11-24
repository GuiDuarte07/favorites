using favorites.Models.DTOs.Folder;
using favorites.Models.Entities;

namespace favorites.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        public Task<Folder> CreateFolderAsync(CreateFolderDTO folder, long UserId);

        public Task<Folder?> UpdateFolderAsync(UpdateFolderDTO folderUpdateInfo);

        public InfoFolderDTO? GetFolder(long id);
    }
}
