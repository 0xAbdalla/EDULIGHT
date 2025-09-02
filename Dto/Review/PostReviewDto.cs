namespace EDULIGHT.Dto.Review
{
    public class PostReviewDto
    {
        public int Course_Id { get; set; }
        public Guid Student_id { get; set; }
        public string Comment { get; set; }
    }
}
