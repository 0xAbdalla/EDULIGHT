using AutoMapper;
using EDULIGHT.Dto.Order;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using EDULIGHT.Services.CartService;
using EDULIGHT.Services.PaymentService;

namespace EDULIGHT.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(ICartService cartService,IUnitOfWork unitOfWork,IPaymentService paymentService,IMapper mapper)
        {
            _cartService = cartService;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _mapper = mapper;
        }
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, int BasketId)
        {
            var basket = await _cartService.GetBasketAsync(BasketId);
            if (basket is null) return null;
            var orderitems = new List<OrderItem>();
            if (basket.items.Count() > 0)
            {
                foreach (var item in basket.items)
                {
                    var course = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsync(item.CourseId);
                    var courseitemorder = new CourseItemOrder(course.CourseId, course.Title, course.PosterURL);
                    var orderitem = new OrderItem(courseitemorder, course.Price);
                    orderitems.Add(orderitem);
                }
            }
            var subtotal = orderitems.Sum(I => I.Price + 5);
            var basketdto = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(BasketId);
            var order = new Order(buyerEmail, orderitems, subtotal, basketdto.PaymentIntentId);
            await _unitOfWork.GetGenericRepositories<Order>().AddAsync(order);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<OrderToReturnDto?> GetOrderByIdForSpecificStudentAsync(string buyerEmail, int orderid)
        {
            var spec = new OrderSpecification(buyerEmail, orderid);
            var order = await _unitOfWork.GetGenericRepositories<Order>().GetByIdAsyncWithSpec(spec);
            if (order == null) return null;
            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>?> GetOrderForSpecificStudentAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var order = await _unitOfWork.GetGenericRepositories<Order>().GetAllAsyncWithSpec(spec);
            if (order == null) return null;
            return (IEnumerable<OrderToReturnDto>)_mapper.Map<OrderToReturnDto>(order);

        }
    }
}
