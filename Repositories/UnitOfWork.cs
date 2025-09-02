using EDULIGHT.Configrations;
using EDULIGHT.Entities.Users;
using EDULIGHT.Repositories.GenericRepository;
using System.Collections;

namespace EDULIGHT.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EdulightDbContext _context;
        private readonly Hashtable _repository;

        public UnitOfWork(EdulightDbContext context)
        {
            _context = context;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<Tentity> GetGenericRepositories<Tentity>() where Tentity : class
        {
            var type = typeof(Tentity).Name;
            if (!_repository.ContainsKey(type))
            {
                var usersrepository = new GenericRepository<Tentity>(_context);
                _repository.Add(type, usersrepository);
            }

            var repository = _repository[type] as IGenericRepository<Tentity>;
            if (repository == null)
            {
                throw new InvalidOperationException($"Repository for type {type} could not be found.");
            }

            return repository;
        }
    }
}
