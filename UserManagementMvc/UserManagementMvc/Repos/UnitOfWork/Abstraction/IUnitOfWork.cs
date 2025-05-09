using UserManagementMvc.Repos.Abstraction.Base;

namespace UserManagementMvc.Repos.UnitOfWork.Abstraction
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>() where TRepository : class, IRepositoryBase;
        Task CommitAsync();
    }
}
