using System.Linq.Expressions;
using UserManagementMvc.Entities.Base;
using UserManagementMvc.Models;

namespace UserManagementMvc.Repos.Abstraction.Base
{
    public interface IRepository<TEntity> : IRepositoryBase where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<PaginationResult<TEntity>> GetAllAsync(int page, int pageSize);
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);
    }
}
