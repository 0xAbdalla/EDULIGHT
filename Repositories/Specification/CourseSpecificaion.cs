using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Repositories.Specification
{
    public class CourseSpecificaion : BaseSpecification<Course>
    {
        public CourseSpecificaion(int id) : base(p=>p.CourseId == id) 
        {
            ApplyIncludes();
        }
        public CourseSpecificaion(GetFiltercourseDto? dto) : base(p=> (!dto.Category_id.HasValue || dto.Category_id == p.category_id)&(!dto.Price.HasValue || dto.Price == p.Price)&(!dto.Level.HasValue || dto.Level == dto.Level))
        {
            ApplyIncludes();
        }
        public CourseSpecificaion(int pageSize, int pageIndex)
        {
            ApplyPagination(pageSize * (pageIndex - 1), pageSize);
            ApplyIncludes();

        }
        public CourseSpecificaion(string? name) : base(p => (string.IsNullOrEmpty(name) || p.Title.ToLower().Contains(name)))
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Review);
            Includes.Add(p => p.sections);
            ThenInclude.Add("sections.ContentCourse");

        }

    }
}
