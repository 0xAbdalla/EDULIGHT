using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Repositories.Specification
{
    public class RoadmapSpecification : BaseSpecification<Roadmap>
    {
        public RoadmapSpecification(int id) : base(p => p.Roadmap_Id == id)
        {
            ApplyIncludes();
        }
        public RoadmapSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Courses);
            ThenInclude.Add("Courses.sections.ContentCourse");
        }
    }
}
