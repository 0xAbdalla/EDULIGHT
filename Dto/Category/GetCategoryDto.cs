using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Dto.Category
{
    public class GetCategoryDto
    {
        public string Name { get; set; }
        public string PosterURL { get; set; }
        public ICollection<GetCoursesDto> Courses { get; set; } = new List<GetCoursesDto>();

    }
}
