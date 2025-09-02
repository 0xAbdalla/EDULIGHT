using EDULIGHT.Dto.Category;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Errors;
using EDULIGHT.Services.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IAppService _appService;
        private new List<String> AllowedExtension = new List<string> { ".jpg", ".png" };
        private long AllowedSize = 10485760;

        public CourseController(IAppService appService)
        {
            _appService = appService;
        }


        [HttpGet("GetAllCoursesCategoriesAsync")]
        public async Task<IActionResult> GetAllCoursesCategoriesAsync(int id)
        {
            if (id == 0) 
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status404NotFound));
            }
            var result = await _appService.GetAllCoursesCategoriesAsync(id);
            return Ok(result);
        }
        [HttpGet("GetAllCoursesRoadmapsAsync")]
        public async Task<IActionResult> GetAllCoursesRoadmapsAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status404NotFound));
            }
            var result = await _appService.GetAllCoursesRoadmapsAsync(id);
            return Ok(result);
        }
        [HttpGet("GetAboutCourseAsync")]
        public async Task<IActionResult> GetAboutCourseAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status404NotFound));
            }
            var result = await _appService.GetAboutCourseAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost("AddCourseAsync")]
        public async Task<IActionResult> AddCourseAsync([FromForm] PostCoursesDto dto)
        {
            var getnameinstructor = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (getnameinstructor == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized,"Unaouthrized"));
            if (!AllowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("The poster extension should be .jpg and .png !!");
            }
            if (dto.Poster.Length > AllowedSize)
            {
                return BadRequest("The Size should be only 10Miga !!");
            }
            var result = await _appService.AddCourseAsync(dto,getnameinstructor);
            return Ok(result);
        }
        [Authorize(Roles = "Instructor")]
        [HttpPatch("UpdateCourseAsync")]
        public async Task<IActionResult> UpdateCourseAsync([FromForm] PostCoursesDto dto)
        {
            //var getnameinstructor = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (getnameinstructor == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Unaouthrized"));
            if (dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Complete The Fields"));
            var updatecourse = await _appService.UpdateCourse(dto);
            if (updatecourse == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found"));
            return Ok(updatecourse);
        }
        [HttpGet("GetFilterationCourse")]
        public async Task<IActionResult> GetFilterationCourse([FromQuery]GetFiltercourseDto dto)
        {
            if (dto.Level == null || dto.Price == null || dto.Category_id == null) 
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Null Input !"));
            var getcourse = await _appService.FilterCoursesAsync(dto);
            if (getcourse == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,"Not Found"));
            return Ok(getcourse);
        }
        [HttpGet("SearchWithCourses")]
        public async Task<IActionResult> SearchWithCourses([FromQuery] string name)
        {
            if (name == null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Null Input !"));
            var getcourse = await _appService.SearchWithCourses(name);
            if (getcourse == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found"));
            return Ok(getcourse);
        }
        [Authorize(Roles ="Instructor")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            if(id == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Null Input !"));
            var remove = await _appService.DeleteCourseAsync(id);
            if(remove == false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found"));
            return Ok($"Removed : {remove}");
        }
        [HttpGet("GetAllCoursesAsync")]
        public async Task<IActionResult> GetAllCoursesAsync([FromQuery]int? pageSize = 24,[FromQuery]int? pageIndex = 1)
        {
            var getallcourse = await _appService.GetAllCoursesAsync(pageSize, pageIndex);
            return Ok(getallcourse);
        }

    }
}
