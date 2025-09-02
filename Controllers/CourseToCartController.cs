using EDULIGHT.Dto.Cart;
using EDULIGHT.Errors;
using EDULIGHT.Services.CourseToCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseToCartController : ControllerBase
    {
        private readonly ICourseToCart _courseToCart;

        public CourseToCartController(ICourseToCart courseToCart)
        {
            _courseToCart = courseToCart;
        }
        [HttpPost]
        public async Task<IActionResult> AddCourseToCart(PostCartDto dto)
        {

            if(dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"invalid"));
            var cart = await _courseToCart.AddCourseToCart(dto);
            return Ok("Added.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCourseFromCart(PostCartDto dto)
        {
            if (dto == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "invalid"));
            var cart = await _courseToCart.DeleteCourseFromCart(dto);
            if(cart == false) return NotFound("Not Found The Cart or Course.");
            return Ok("Removed.");
        }
    }
}
