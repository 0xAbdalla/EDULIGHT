using EDULIGHT.Dto.ContentCourse;

namespace EDULIGHT.Dto.Section
{
    public class ReturnSectionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<GetContentCourseDto> ContentCourse { get; set; }
    }
}
