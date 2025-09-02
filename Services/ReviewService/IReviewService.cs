using EDULIGHT.Dto.Review;

namespace EDULIGHT.Services.ReviewService
{
    public interface IReviewService
    {
        Task<GetReviewDto> AddReview(PostReviewDto dto);
        Task<bool> DeleteReview(int reviewid);
        Task<GetReviewDto> EditReveiw(EditReviewDto dto);
        Task<IEnumerable<GetReviewDto>> GetReviewsInCourse(int courseid);
    }
}
