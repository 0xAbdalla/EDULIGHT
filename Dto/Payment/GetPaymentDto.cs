using EDULIGHT.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Payment
{
    public class GetPaymentDto
    {

        [ForeignKey("Course")]
        public int course_id { get; set; }
        public decimal PriceCourse { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
