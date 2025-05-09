using UserManagementMvc.Servicess.Abstraction;
using UserManagementMvc.Servicess.Implementation;

namespace UserManagementMvc.Extensions
{
    public static class AddAllServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, EmailService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IGroupService, GroupService>();
        }
    }
}
