using EDULIGHT.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Payment
{
    public class PostPaymentDto
    {
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
