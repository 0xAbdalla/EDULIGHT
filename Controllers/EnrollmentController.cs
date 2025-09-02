using AutoMapper;
using EDULIGHT.Dto.Enrollment;
using EDULIGHT.Entities;
using EDULIGHT.Errors;
using EDULIGHT.Services.EnrollmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentService enrollmentService,IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddEnrollment(PostEnrollmentDto dto)
        {
            if (dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var map = _mapper.Map<Enrollment>(dto);
            var addenroll = await _enrollmentService.AddEnrollment(map);
            if (addenroll == null) return BadRequest(new ApiErrorResponse(400,"Already Enroll In This Course."));
            return Ok(addenroll);
        }
        [Authorize(Roles = "Student")]
        [HttpGet("GetMyCourseStudent")]
        public async Task<IActionResult> GetMyCourseStudent()
        {
            var studentIdClaim = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(studentIdClaim)) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized,"UN_UTHORIZED"));
            var getmycourse = await _enrollmentService.GetMyCourseStudent(studentIdClaim);
            if(getmycourse == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(getmycourse);
        }
        [HttpGet("GetNumberOfStudentInCourse")]
        public async Task<IActionResult> GetNumberOfStudentInCourse(int courseid)
        {
            if (courseid == 0) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,"Not Found The Course."));
            var getnumberofstudent = await _enrollmentService.GetNumberOfStudentInCourse(courseid);
            if(getnumberofstudent == 0) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,"Not Found Any Enrollment In The Course"));
            return Ok(getnumberofstudent);
        }
    }
}
