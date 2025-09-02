using AutoMapper;
using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.Enroll_Pay;
using EDULIGHT.Entities.Users;
using EDULIGHT.Errors;
using EDULIGHT.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService,IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(int cartid) 
        {
            if (cartid == null) return BadRequest(new ApiErrorResponse(400,"invalid"));
            var cart = await _cartService.GetBasketAsync(cartid);
            if (cart == null)  new Cart() { Id = cartid};
            return Ok(cart);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart(ReturnCartDto dto)
        {
            //var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var mapcart = _mapper.Map<Cart>(dto);
            var cart = await _cartService.CreateBasketAsync(mapcart);
            if (cart == null) return BadRequest(new ApiErrorResponse(400, "invalid"));
            return Ok(cart);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int cartid)
        {
            await _cartService.DeleteBasketAsync(cartid);
            var cart = await _cartService.GetBasketAsync(cartid);
            if (cart != null) return BadRequest(new ApiErrorResponse(400,"invalid"));
            return Ok("Deleted");
        }
    }
}
