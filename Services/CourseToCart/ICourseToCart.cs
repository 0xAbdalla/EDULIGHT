using EDULIGHT.Dto.Cart;

namespace EDULIGHT.Services.CourseToCart
{
    public interface ICourseToCart
    {
        Task<bool> AddCourseToCart(PostCartDto dto);
        Task<bool> DeleteCourseFromCart(PostCartDto dto);
    }
}
