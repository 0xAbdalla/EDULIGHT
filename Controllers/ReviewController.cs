using EDULIGHT.Dto.Review;
using EDULIGHT.Errors;
using EDULIGHT.Services.ReviewService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(PostReviewDto dto)
        {
            if (dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var addreview = await _reviewService.AddReview(dto);
            if (addreview == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(addreview);
        }
        [HttpPut]
        public async Task<IActionResult> EditReview(EditReviewDto dto)
        {
            if (dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var editreview = await _reviewService.EditReveiw(dto);
            if (editreview == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(editreview);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int reviewid)
        {
            if (reviewid == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var deletereview = await _reviewService.DeleteReview(reviewid);
            if (deletereview == false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok("Deleted");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReviewsInCourse(int courseid)
        {
            if (courseid == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var getallreview = await _reviewService.GetReviewsInCourse(courseid);
            if (getallreview == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(getallreview);

        }
    }
}
