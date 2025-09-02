using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities
{
    public enum PaymentMethod
    {
        PayPal,
        Stripe
    }
    public class Payment
    {
        [Key]
        public int Payment_Id { get; set; }
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public Course Course { get; set; }
        public required double PriceCourse { get; set; }
        public required double TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now; 
    }
}
