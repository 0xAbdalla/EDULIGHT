using EDULIGHT.Dto.Category;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Roadmap;

namespace EDULIGHT.Services.AppService
{
    public interface IAppService
    {
        Task<IEnumerable<GetCoursesDto>> GetAllCoursesAsync(int? pageSize, int? pageIndex);
        Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<GetCoursesDto>> GetAllCoursesCategoriesAsync(int id);
        Task<IEnumerable<GetRoadmapDto>> GetAllRoadmapsAsync();
        Task<GetCoursesDto> GetAboutCourseAsync(int id);
        Task<IEnumerable<GetCoursesDto>> FilterCoursesAsync(GetFiltercourseDto? dto);
        Task<GetCategoryDto> AddCategoryAsync(PostCategoryDto dto);
        Task<GetCoursesDto> AddCourseAsync(PostCoursesDto dto,string instructorname);
        Task<GetCoursesDto> UpdateCourse(PostCoursesDto dto);
        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<GetCoursesDto>> SearchWithCourses(string? name);
        Task<IEnumerable<GetCoursesDto>> GetAllCoursesRoadmapsAsync(int id);




    }
}
