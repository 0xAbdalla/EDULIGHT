using EDULIGHT.Entities.ContentData;

namespace EDULIGHT.Dto.Cart
{
    public class CartItemDto
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
