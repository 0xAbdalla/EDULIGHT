namespace EDULIGHT.Entities.Enroll_Pay
{
    public class CourseItemOrder
    {
        public CourseItemOrder()
        {
            
        }

        public CourseItemOrder(int courseid,string coursename,string pictureurl)
        {
            CourseId = courseid;
            CourseName = coursename;
            PictureUrl = pictureurl;
        }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }

    }
}
