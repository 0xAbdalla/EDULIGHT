using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.ContentData
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public IFormFile Poster { get; set; }
        public string PosterURL { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
