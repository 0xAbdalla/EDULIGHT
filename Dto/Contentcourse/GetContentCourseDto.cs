using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.ContentCourse
{
    public class GetContentCourseDto
    {
        public int Content_Id { get; set; }

        public string ContentTitle { get; set; }
        public string ContentType { get; set; }
        public string ContentUrl { get; set; }
        public DateTime Created_at { get; set; }
    }
}
