using EDULIGHT.Dto.ContentCourse;

namespace EDULIGHT.Dto.Section
{
    public class sectionDto
    {
        public string Title { get; set; }
        public ICollection<PostContentCourseDto> ContentCourse { get; set; }
    }
}
