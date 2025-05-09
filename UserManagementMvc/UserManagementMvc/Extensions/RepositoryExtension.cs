using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.Implementation;
using UserManagementMvc.Repos.UnitOfWork.Abstraction;
using UserManagementMvc.Repos.UnitOfWork.Implementation;

namespace UserManagementMvc.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }
}
