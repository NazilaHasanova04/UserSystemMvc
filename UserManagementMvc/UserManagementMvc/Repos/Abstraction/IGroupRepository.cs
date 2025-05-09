using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Repos.Abstraction.Base;

namespace UserManagementMvc.Repos.Abstraction
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<PaginationResult<Group>> GetGroupsWithTeacherId(int id, int pageNumber, int pageSize);
        Task<PaginationResult<Group>> GetAllGroupsWithSubjId(int id, int pageNumber, int pageSize);
        Task<PaginationResult<Group>> GetAllGroupsWithCourseId(int id, int pageNumber, int pageSize);
    }
}
