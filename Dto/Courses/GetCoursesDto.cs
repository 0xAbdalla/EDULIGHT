using EDULIGHT.Dto.Review;
using EDULIGHT.Dto.Section;
using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Courses
{
    public class GetCoursesDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PosterUrl { get; set; }
        public int Duration { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }
        public string InstructorId { get; set; }
        public ICollection<ReturnSectionDto> sections { get; set; } = new List<ReturnSectionDto>();
        public ICollection<GetReviewDto>? Review { get; set; } = new List<GetReviewDto>();
        public DateTime Created_at { get; set; }
        public int category_id { get; set; }
        public int roadmap_id { get; set; }

    }

    public class GetFiltercourseDto
    {
        [ForeignKey("Category")]
        public int? Category_id { get; set; }
        public decimal? Price { get; set; }
        public Level? Level { get; set; }
    }

}
