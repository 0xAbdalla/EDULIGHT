namespace EDULIGHT.Dto.Review
{
    public class GetReviewDto
    {
        public int Course_Id { get; set; }
        public Guid Student_id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
