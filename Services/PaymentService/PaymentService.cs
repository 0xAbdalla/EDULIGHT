using AutoMapper;
using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using EDULIGHT.Services.CartService;
using Stripe;

namespace EDULIGHT.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentService(ICartService cartService,IUnitOfWork unitOfWork,IConfiguration configuration,IMapper mapper)
        {
            _cartService = cartService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ReturnCartDto> CreateOrUpdatePaymentIntentIdAsync(int cartid)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            var basket = _mapper.Map<Cart>(await _cartService.GetBasketAsync(cartid));
            if (basket == null) return null;
            var subtotal = 0m;
            if (basket.items.Count() > 0)
            {
                foreach (var item in basket.items)
                {
                    var spec = new CourseSpecificaion(item.CourseId);
                    var itemcourse = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsyncWithSpec(spec);
                    subtotal = subtotal + itemcourse.Price + 5;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //create PaymentIntentId
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subtotal * 100),
                    PaymentMethodTypes = new List<string>() { "card"},
                    Currency = "usd"
                };
                paymentIntent = await service.CreateAsync(option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                //update PaymentIntentId
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subtotal * 100)
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            var updatebasket = await _cartService.CreateBasketAsync(basket);
            if (updatebasket == null) return null;
            return updatebasket;
        }
    }
}
