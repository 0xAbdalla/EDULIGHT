using AutoMapper;
using EDULIGHT.Dto.Order;
using EDULIGHT.Errors;
using EDULIGHT.Services.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(int basketId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.CreateOrderAsync(userEmail, basketId);
            if (order == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        [HttpGet("GetOrderForSpecificUser")]
        public async Task<IActionResult> GetOrderForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrderForSpecificStudentAsync(userEmail);
            if (order == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(order));
        }

        [HttpGet("GetOrderByIdForSpecificStudentAsync")]
        public async Task<IActionResult> GetOrderByIdForSpecificStudentAsync(int orderid)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrderByIdForSpecificStudentAsync(userEmail,orderid);
            if (order == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }


    }
}
