using EDULIGHT.Dto.Category;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Roadmap;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Repositories.Specification;
using Microsoft.EntityFrameworkCore;

namespace EDULIGHT.Repositories.GenericRepository
{
    public interface IGenericRepository<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> GetAllAsyncWithSpec(ISpecification<Tentity> spec);
        Task<Tentity> GetByIdAsyncWithSpec(ISpecification<Tentity> spec);
        Task<IEnumerable<Tentity>> GetAllAsync();
        Task<Tentity> GetByIdAsync(int id);
        Task<Tentity> AddAsync(Tentity tentity);
        //Task<Tentity> Update(Tentity entity);
        Task Delete(int id);
        Task<IEnumerable<Course>> GetCoursesCategoriesAsync(int id);
        Task<IEnumerable<Course>> GetCoursesRoadmapsAsync(int id);

        //Task<IEnumerable<Course>> GetFilterCourses(GetFiltercourseDto dto);


    }
}
