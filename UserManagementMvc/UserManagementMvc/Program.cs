using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Extensions;
using UserManagementMvc.Mappings;

namespace UserManagementMvc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityConfigs();

            builder.Services.AddHttpClientConfigs();

            builder.Services.AddServices();

            builder.Services.AddRepositories();

            builder.Services.AddAutoMapper(typeof(MappingConfiguration).Assembly);

            var app = builder.Build();

            await app.Services.AddSeedData();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
