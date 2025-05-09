using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Repos.Abstraction;

namespace UserManagementMvc.Repos.Implementation
{
    public class FileRepository : IFileRepository
    {

        private readonly UserDbContext _studentDbContext;

        public FileRepository(UserDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task AddAsync(FileDetails file)
        {
            _studentDbContext.FileDetails.Add(file);

            await _studentDbContext.SaveChangesAsync();
        }

        public async Task<FileDetails> GetByIdAsync(int id)
        {
            return await _studentDbContext.FileDetails.FindAsync(id);
        }
    }
}
