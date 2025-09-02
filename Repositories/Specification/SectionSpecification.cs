using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Repositories.Specification
{
    public class SectionSpecification : BaseSpecification<Sections>
    {
        public SectionSpecification(int id) : base(s=>s.Id == id)
        {
            ApplyIncludes();
            
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.ContentCourse);
        }
    }
}
