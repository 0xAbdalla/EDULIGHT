using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Dto.Order
{
    public class OrderToReturnDto
    {
        public int OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string Status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntentId { get; set; } = string.Empty;

    }
}
