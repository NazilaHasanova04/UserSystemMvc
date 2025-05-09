using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;

namespace UserManagementMvc.Servicess.Abstraction
{
    public interface IUserService
    {
        Task<ApiResponse<User>> RegisterAsync(RegisterModel model);
        Task<ApiResponse<User>> LoginAsync(LoginModel model);
        Task Logout();
        Task<ApiResponse<User>> ChangePasswordAsync(UpdatePasswordModel model);
        Task<ApiResponse<User>> ForgotPasswordAsync(ForgotPasswordModel model);
        Task<ApiResponse<User>> ConfirmRegisterAsync(string userId, string token);

    }
}
