using Microsoft.EntityFrameworkCore;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.Implementation.Base;

namespace UserManagementMvc.Repos.Implementation
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly UserDbContext _context;

        public StudentRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Student?> StudentWithFile(int id)
        {
            return await _context.Students
                .Include(s => s.StudentFiles)
                .ThenInclude(s => s.FileDetails)
                .FirstOrDefaultAsync(s => s.Id == id);

        }


        public async Task<PaginationResult<Student>> GetStudentsLessonsInGroup(int groupId, int pageNumber, int pageSize)
        {
            IQueryable<Student> query = _context.Students
                .Include(s => s.StudentGroups)
                    .ThenInclude(sg => sg.Group)
                .Include(s => s.StudentLessons)
                    .ThenInclude(sl => sl.Lesson)
                .Where(s => s.StudentGroups.Any(sg => sg.GroupId == groupId));

            int count = await query.CountAsync();


            IEnumerable<Student> students = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return new PaginationResult<Student>(count, students);
        }

        public async Task<PaginationResult<Student>> GetStudentsByGroupId(int id, int pageNumber, int pageSize)
        {
            IQueryable<Student> query = _context.Students.Include(x => x.StudentGroups)
                .ThenInclude(x => x.Group)
                .Where(x => x.StudentGroups
                .Any(x => x.GroupId == id));

            int count = await query.CountAsync();

            IEnumerable<Student> students = await query.Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).ToListAsync();

            return new PaginationResult<Student>(count, students);
        }
    }
}
