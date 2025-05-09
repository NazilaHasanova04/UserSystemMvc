using Microsoft.EntityFrameworkCore;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.Implementation.Base;

namespace UserManagementMvc.Repos.Implementation
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly UserDbContext _context;

        public GroupRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginationResult<Group>> GetGroupsWithTeacherId(int id, int pageNumber, int pageSize)
        {

            IQueryable<Group> query = _context.Groups.Include(x => x.Course)
                                              .Include(x => x.Subject)
                                              .Include(x => x.Teacher)
                                              .Where(x => x.TeacherId == id);

            int count = await query.CountAsync();

            var groups = await query.Skip((pageNumber - 1) * pageSize)
             .Take(pageSize).ToListAsync();

            return new PaginationResult<Group>(count, groups);
        }

        public async Task<PaginationResult<Group>> GetAllGroupsWithSubjId(int id, int pageNumber, int pageSize)
        {
            IQueryable<Group> query = _context.Groups.Where(x => x.SubjectId == id);

            int count = await query.CountAsync();

            var groups = await query.Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return new PaginationResult<Group>(count, groups);
        }

        public async Task<PaginationResult<Group>> GetAllGroupsWithCourseId(int id, int pageNumber, int pageSize)
        {
            IQueryable<Group> query = _context.Groups
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Where(x => x.CourseId == id);

            int count = await query.CountAsync();


            IEnumerable<Group> groups = await query.Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync();

            return new PaginationResult<Group>(count, groups);
        }

    }
}
