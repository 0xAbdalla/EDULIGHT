using EDULIGHT.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.ContentData
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey("Course")]
        public int Course_Id { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Student")]
        public string Student_id { get; set; }
    }
}
