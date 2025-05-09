
using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Repos.Abstraction.Base;

namespace UserManagementMvc.Repos.Abstraction
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<PaginationResult<Student>> GetStudentsLessonsInGroup(int groupId, int pageNumber, int pageSize);
        Task<PaginationResult<Student>> GetStudentsByGroupId(int id, int pageNumber, int pageSize);
        Task<Student?> StudentWithFile(int id);

    }
}
