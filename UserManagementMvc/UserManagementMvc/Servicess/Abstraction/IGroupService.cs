using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Models.Group;
using UserManagementMvc.Models.Pagination;

namespace UserManagementMvc.Servicess.Abstraction
{
    public interface IGroupService
    {
        Task<ApiResponse<bool>> CreateAsync(CreateGroupDto groupDto);
        Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetAllAsync(PaginationDto paginationDto);
        Task<ApiResponse<GroupDto>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> UpdateAsync(int id, UpdateGroupDto groupDto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetGroupsWithTeacherId(int id, PaginationDto paginationDto);
        Task<ApiResponse<PaginationResponseDto<GroupsWithSubIdDto>>> GetAllGroupsWithSubjId(int id, PaginationDto paginationDto);
        Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetAllGroupsWithCourseId(int id, PaginationDto paginationDto);
    }
}
