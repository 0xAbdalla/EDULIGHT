using EDULIGHT.Entities.Enroll_Pay;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Dto.Cart
{
    public class PostCartItemDto
    {

        public int cartId { get; set; }
        public int courseid { get; set; }

    }
}
