
using AutoMapper;
using EDULIGHT.Configrations;
using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using EDULIGHT.Services.CartService;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace EDULIGHT.Services.CourseToCart
{
    public class CourseToCart : ICourseToCart
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly EdulightDbContext context;

        public CourseToCart(IUnitOfWork unitOfWork,ICartService cartService,IMapper mapper,EdulightDbContext context)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _mapper = mapper;
            this.context = context;
        }
        public async Task<bool> AddCourseToCart(PostCartDto dto)
        {
            var cart = await _cartService.GetBasketAsync(dto.cartId);
            var mappedcart = _mapper.Map<Cart>(cart);

            if (cart == null)
            {
                var ccart = new Cart() { StudentId = "46046e50-f746-4546-ba00-98f7037f3eda"  };
                await _cartService.CreateBasketAsync(ccart);
                await _unitOfWork.CompleteAsync();

            }

            var mappeditem = _mapper.Map<CartItem>(dto);
            var getcourse = await _unitOfWork.GetGenericRepositories<CartItem>().AddAsync(mappeditem);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteCourseFromCart(PostCartDto dto)
        {
            var cartid =  _mapper.Map<Cart>(await _cartService.GetBasketAsync(dto.cartId));
            if (cartid == null) { return false; }
            var getcartitem = await context.Set<CartItem>().FirstOrDefaultAsync(ci => ci.CartId == dto.cartId && ci.CourseId == dto.courseid);
            if (getcartitem == null) { return false; }
            context.Set<CartItem>().Remove(getcartitem);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
