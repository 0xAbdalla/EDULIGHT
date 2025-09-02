using EDULIGHT.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Enrollment
{
    public class GetEnrollmentDto
    {
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Status Status { get; set; }
    }
}
