namespace EDULIGHT.Dto.Cart
{
    public class PostCartWithoutId
    {
        public string StudentId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
