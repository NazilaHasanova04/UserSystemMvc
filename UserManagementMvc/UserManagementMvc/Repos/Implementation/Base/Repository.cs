using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagementMvc.Data;
using UserManagementMvc.Entities.Base;
using UserManagementMvc.Models;
using UserManagementMvc.Repos.Abstraction.Base;

namespace UserManagementMvc.Repos.Implementation.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly UserDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(UserDbContext studentDbContext)
        {
            _dbContext = studentDbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<PaginationResult<TEntity>> GetAllAsync(int page, int pageSize)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            int count = await query.CountAsync();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            IEnumerable<TEntity> entities = await query.ToListAsync();

            return new PaginationResult<TEntity>(count, entities);
        }


        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet.Where(predicate);
            return query;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
