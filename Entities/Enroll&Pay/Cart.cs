using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.Enroll_Pay
{
    public class Cart
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public ICollection<CartItem> items { get; set; } = new List<CartItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
