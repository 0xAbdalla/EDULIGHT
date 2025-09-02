using EDULIGHT.Dto.Cart;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Dto.Basket
{
    public class ReturnCartDto
    {
        public ReturnCartDto() { }

        public ReturnCartDto(string student_id, ICollection<CartItemDto> items, int id)
        {
            this.StudentId = student_id;
            this.items = items;
            Id = id;
        }
        public int Id { get; set; }
        public string StudentId { get; set; }
        public ICollection<CartItemDto> items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
