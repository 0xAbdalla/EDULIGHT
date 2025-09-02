using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.ContentData
{
    public class Roadmap
    {
        [Key]
        public int Roadmap_Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Poster { get; set; }
        public string PosterURL { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }

    }
}
