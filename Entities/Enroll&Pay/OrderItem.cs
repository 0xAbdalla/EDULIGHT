namespace EDULIGHT.Entities.Enroll_Pay
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }
        public OrderItem(CourseItemOrder course, decimal price)
        {
            Course = course;
            Price = price;
        }
        public int OrderItemId { get; set; }
        public CourseItemOrder Course { get; set; }
        public decimal Price { get; set; }

    }
}
