using System.Runtime.Serialization;

namespace EDULIGHT.Entities.Enroll_Pay
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Recieved")]
        PaymentRecieved,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
    public class Order
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public int OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal;
        public string PaymentIntentId { get; set; }

    }
}
