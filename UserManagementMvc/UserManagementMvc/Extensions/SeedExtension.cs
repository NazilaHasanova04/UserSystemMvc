using Microsoft.AspNetCore.Identity;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;

namespace UserManagementMvc.Extensions
{
    public static class SeedExtension
    {
        public static async Task AddSeedData(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                await SeedData.Seed(roleManager, userManager);
            }
        }
    }
}
