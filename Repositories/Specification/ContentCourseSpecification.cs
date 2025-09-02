using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Repositories.Specification
{
    public class ContentCourseSpecification : BaseSpecification<ContentCourse>
    {
        public ContentCourseSpecification(int id) : base(p => p.Content_Id == id)
        {
            ApplyIncludes();
        }
        public ContentCourseSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
        }
    }
}
