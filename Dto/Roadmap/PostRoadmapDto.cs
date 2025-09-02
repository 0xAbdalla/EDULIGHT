using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Roadmap
{
    public class PostRoadmapDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Poster { get; set; }
        [ForeignKey("Category")]
        public int Category_id { get; set; }
    }
}
