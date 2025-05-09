using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Models.Student;

namespace UserManagementMvc.Servicess.Abstraction
{
    public interface IStudentService
    {
        Task<ApiResponse<bool>> CreateAsync([FromForm] CreateStudentDto studentDto);
        Task<ApiResponse<bool>> CreateMvcAsync([FromForm] CreateStudentDto studentDto);
        Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetAllAsync(PaginationDto paginationDto);
        Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetAllMvcAsync(PaginationDto paginationDto);
        Task<ApiResponse<StudentDto>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> UpdateAsync(int id, UpdateStudentDto studentDto);
        Task<ApiResponse<bool>> UpdateMvcAsync(int id, UpdateStudentDto studentDto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<bool>> DeleteMvcAsync(int id);
        Task<ApiResponse<bool>> UpdateGroupsAsync(int id, UpdateStudentGroupDto stdGroup);
        Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetStudentsByGroupId(int id, PaginationDto paginationDto);
        Task<ApiResponse<StudentWithFileDto>> GetStudentFileDetails(int id);
    }
}
