using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Enrollment
{
    public class PostEnrollmentDto
    {
        public int course_id { get; set; }
        public string Student_id { get; set; }

    }
}
