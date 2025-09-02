using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.ContentData
{
    public class Sections
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<ContentCourse> ContentCourse { get; set; } = new List<ContentCourse>();
        public DateTime Created_at => DateTime.Now;
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
