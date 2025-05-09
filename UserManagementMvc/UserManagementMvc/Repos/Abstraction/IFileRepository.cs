using UserManagementMvc.Entities;

namespace UserManagementMvc.Repos.Abstraction
{
    public interface IFileRepository
    {
        Task AddAsync(FileDetails file);
        Task<FileDetails> GetByIdAsync(int id);
    }
}
