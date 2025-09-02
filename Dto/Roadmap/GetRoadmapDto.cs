using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Roadmap
{
    public class GetRoadmapDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public string PosterURL { get; set; }
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public ICollection<GetCoursesDto> Courses { get; set; } = new List<GetCoursesDto>();


    }
}
