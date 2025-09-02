
using EDULIGHT.Configrations;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Repositories.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EDULIGHT.Repositories.GenericRepository
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : class
    {
        private readonly EdulightDbContext _context;

        public GenericRepository(EdulightDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tentity>> GetAllAsyncWithSpec(ISpecification<Tentity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<Tentity> GetByIdAsyncWithSpec(ISpecification<Tentity> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<Tentity> AddAsync(Tentity entity)
        {
           await _context.AddAsync(entity);
            return entity;
        }
        public async Task Delete(int id)
        {
            var entity = await _context.Set<Tentity>().FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }
        //public async Task<Tentity> Update(Tentity entity)
        //{

        //    if (entity != null)
        //    {
        //         _context.Set<Tentity>().Update(entity);
        //    }
        //    return entity;
        //}
        private IQueryable<Tentity> ApplySpecification(ISpecification<Tentity> spec)
        {
            return SpecificationEvaluator<Tentity>.GetQuery(_context.Set<Tentity>(), spec);
        }
        public async Task<Tentity> GetByIdAsync(int id)
        {
             return await _context.Set<Tentity>().FindAsync(id);
        }
        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
           return await _context.Set<Tentity>().ToListAsync(); 
        }
        public async Task<IEnumerable<Course>> GetCoursesCategoriesAsync(int id)
        {
            return await _context.Courses.Where(p=>p.category_id == id).ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetCoursesRoadmapsAsync(int id)
        {
            return await _context.Courses.Where(p => p.roadmap_id == id).OrderBy(p=>p.Level == Level.Beginner? "Beginner" : p.Level == Level.Intermediate? "Intermediate" : "Advanced").ToListAsync();
        }

    }
}
