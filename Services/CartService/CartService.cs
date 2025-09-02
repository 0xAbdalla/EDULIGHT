using AutoMapper;
using EDULIGHT.Configrations;
using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.Enroll_Pay;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using Microsoft.EntityFrameworkCore;

namespace EDULIGHT.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EdulightDbContext _context;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork,EdulightDbContext context,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteBasketAsync(int basketid)
        {
            await _unitOfWork.GetGenericRepositories<Cart>().Delete(basketid);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ReturnCartDto?> GetBasketAsync(int basketid)
        {

            var spec = new CartSpecification(basketid);
            var basket = await _unitOfWork.GetGenericRepositories<Cart>().GetByIdAsyncWithSpec(spec);
            if (basket == null) return null;
            return _mapper.Map<ReturnCartDto>(basket);
        }

        public async Task<ReturnCartDto?> CreateBasketAsync(Cart cart)
        {
            var spec = new CartSpecification(cart.Id);
            var getcart = await _unitOfWork.GetGenericRepositories<Cart>().GetByIdAsyncWithSpec(spec);
           // var getcart =  _mapper.Map<Cart>(await GetBasketAsync(cart.Id));
            if (getcart == null)
            {
                //var maptowithoutid = _mapper.Map<PostCartWithoutId>(cart);
                //var maptocart = _mapper.Map<Cart>(maptowithoutid);
                var addcart = await _unitOfWork.GetGenericRepositories<Cart>().AddAsync(cart);
                var result = await _unitOfWork.CompleteAsync();
                return _mapper.Map<ReturnCartDto>(addcart);
            }
            else
            {
                getcart.ClientSecret = cart.ClientSecret;
                getcart.PaymentIntentId = cart.PaymentIntentId;
                getcart.items = cart.items;
                var result = await _unitOfWork.CompleteAsync();
                return _mapper.Map<ReturnCartDto>(getcart);
            }
        }
    }
}
