using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.ContentCourse
{
    public class PostContentCourseDto
    {
        public string ContentTitle { get; set; }
        public ContentType ContentType { get; set; }
        public IFormFile Content { get; set; }
    }
}
