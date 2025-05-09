using UserManagementMvc.Data;
using UserManagementMvc.Repos.Abstraction.Base;
using UserManagementMvc.Repos.UnitOfWork.Abstraction;

namespace UserManagementMvc.Repos.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;
        private readonly Dictionary<Type, IRepositoryBase> _repositories;
        public UnitOfWork(UserDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, IRepositoryBase>();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepositoryBase
        {
            Type repositoryImplementationType = GetConcreteRepositoryInfo<TRepository>();
            if (_repositories.TryGetValue(repositoryImplementationType, out IRepositoryBase existingRepo))
            {
                return (TRepository)existingRepo;
            }

            IRepositoryBase repositoryInstance = (IRepositoryBase)Activator
                .CreateInstance(repositoryImplementationType, _context);

            if (repositoryInstance is TRepository repository)
            {
                _repositories.Add(repositoryImplementationType, repository);
                return repository;
            }

            throw new InvalidOperationException($"Could not create repository of type {repositoryImplementationType}");
        }
        private Type GetConcreteRepositoryInfo<TRepository>()
        {
            string interfaceName = typeof(TRepository).Name;
            string className = interfaceName.StartsWith("I") ? interfaceName.Substring(1) : interfaceName;

            string interfaceNamespace = typeof(TRepository).Namespace;
            string implementationFullName = $"{interfaceNamespace}.{className}";

            Type repositoryType = Type.GetType(implementationFullName);
            if (repositoryType == null)
            {
                repositoryType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(t => t.Name == className && typeof(TRepository).IsAssignableFrom(t));
            }

            if (repositoryType == null)
            {
                throw new InvalidOperationException("No concrete class found for");
            }

            return repositoryType;
        }
    }
}
