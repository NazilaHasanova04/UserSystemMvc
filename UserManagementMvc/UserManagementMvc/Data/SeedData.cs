using Microsoft.AspNetCore.Identity;
using UserManagementMvc.Entities;
using UserManagementMvc.Enums;
using UserManagementMvc.Statics;

namespace UserManagementMvc.Data
{
    public class SeedData
    {
        public static async Task Seed(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            foreach (Roles roleType in Enum.GetValues(typeof(Roles)))
            {
                string role = roleType.ToString();

                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }

            User admin = await userManager.FindByEmailAsync(Configurations.adminMail);

            if (admin != null && !await userManager.IsInRoleAsync(admin, Roles.Admin.ToString()))
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

        }
    }
}
