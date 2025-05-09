
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Mappings;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.Implementation;
using UserManagementMvc.Repos.UnitOfWork.Abstraction;
using UserManagementMvc.Repos.UnitOfWork.Implementation;
using UserManagementMvc.Servicess.Abstraction;
using UserManagementMvc.Servicess.Implementation;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("MvcPolicy", (config) =>
                {
                    config.WithOrigins("https://localhost:7122")
                    .AllowAnyHeader()
                    .WithMethods("GET")
                    .AllowCredentials();
                });
            });

            builder.Services.AddDbContext<UserDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));

            builder.Services.AddHttpClient();
            //services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMailService, EmailService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IGroupService, GroupService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAutoMapper(typeof(MappingConfiguration).Assembly);

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            app.UseCors("MvcPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
