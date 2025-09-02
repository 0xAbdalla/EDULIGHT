using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ReturnCartDto> CreateOrUpdatePaymentIntentIdAsync(int cartid);
    }
}
