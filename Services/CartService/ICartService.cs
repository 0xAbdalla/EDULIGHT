using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Services.CartService
{
    public interface ICartService
    {
        Task<ReturnCartDto?> GetBasketAsync(int basketid);
        Task<ReturnCartDto?> CreateBasketAsync(Cart cart);

        Task DeleteBasketAsync(int basketid);

    }
}
