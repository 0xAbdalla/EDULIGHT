using EDULIGHT.Dto.Section;
using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Courses
{
    public class PostCoursesDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Poster { get; set; }
        public int Duration { get; set; }
        public Level Level { get; set; }
        public Language Language { get; set; }
        public string Category { get; set; }
        public List<sectionDto> Sections { get; set; }
        public string WelcomeMessage { get; set; }
        public string CongratulationMessage { get; set; }
    }
}
