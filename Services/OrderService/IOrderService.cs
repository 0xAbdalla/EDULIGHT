using EDULIGHT.Dto.Order;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, int BasketId);
        Task<IEnumerable<OrderToReturnDto>?> GetOrderForSpecificStudentAsync(string buyerEmail);
        Task<OrderToReturnDto?> GetOrderByIdForSpecificStudentAsync(string buyerEmail,int orderid);


    }
}
