using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementMvc.Entities;
using UserManagementMvc.Entities.Base;

namespace UserManagementMvc.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAndRoles> UserAndRoles { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentFile> StudentFiles { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        var deletedEntity = entry.Entity as IDeleteEntity;
                        deletedEntity.IsDeleted = true;
                        deletedEntity.DeleteUserId = 1;
                        deletedEntity.DeletedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        var updatedEntity = entry.Entity as IUpdateEntity;
                        updatedEntity.UpdatedDate = DateTime.UtcNow;
                        updatedEntity.UpdateUserId = 1;
                        break;
                    case EntityState.Added:
                        var addedEntity = entry.Entity as ICreateEntity;
                        addedEntity.CreatedDate = DateTime.UtcNow;
                        addedEntity.CreateUserId = 1;
                        break;

                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        }
    }
}
