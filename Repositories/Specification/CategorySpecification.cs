using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Repositories.Specification
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(int id) : base(p => p.Category_Id == id)
        {
            ApplyIncludes();
        }
        public CategorySpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Courses);
            ThenInclude.Add("Courses.sections.ContentCourse");
        }
    }
}
