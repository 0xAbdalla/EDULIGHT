using EDULIGHT.Entities.Users;
using EDULIGHT.Repositories.GenericRepository;

namespace EDULIGHT.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<Tentity> GetGenericRepositories<Tentity>() where Tentity : class;

    }
}
