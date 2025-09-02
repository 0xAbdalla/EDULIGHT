using EDULIGHT.Entities.ContentData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.Enroll_Pay
{
    public class CartItem
    {
        public int Id { get; set; }
        
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        //public string CourseName { get; set; }
        //public string PictureUrl { get; set; }
        //public string InstructorName { get; set; }
        //public decimal Price { get; set; }

    }
}
