using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities
{
    public enum Status
    {
        Registered,
        Finished
    }
    public class Enrollment
    {
        [Key]
        public int Enrollment_Id { get; set; }
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Student")]

        public string Student_id { get; set; }
        public DateTime EnrollmentDate => DateTime.Now;
        public Status Status { get; set; } = Status.Registered; 


    }
}
