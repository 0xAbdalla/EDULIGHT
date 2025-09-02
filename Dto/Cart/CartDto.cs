using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Dto.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public ICollection<CartItemDto> items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
