using AutoMapper;
using EDULIGHT.Dto.Review;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using Humanizer;

namespace EDULIGHT.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetReviewDto> AddReview(PostReviewDto dto)
        {
            var getidcourse = new CourseSpecificaion(dto.Course_Id);
            var getcourse = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsyncWithSpec(getidcourse);
            if (getcourse == null) return null;
            var addreview = await _unitOfWork.GetGenericRepositories<Review>().AddAsync(_mapper.Map<Review>(dto));
            if(addreview == null) return null;
            var result = await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetReviewDto>(addreview);

        }

        public async Task<bool> DeleteReview(int reviewid)
        {
            var getreview = await _unitOfWork.GetGenericRepositories<Review>().GetByIdAsync(reviewid);
            if (getreview == null) return false;
            await _unitOfWork.GetGenericRepositories<Review>().Delete(reviewid);
            var result = await _unitOfWork.CompleteAsync();
            if(result == 0) return false;
            return true;
        }

        public async Task<GetReviewDto> EditReveiw(EditReviewDto dto)
        {
            var getreview = await _unitOfWork.GetGenericRepositories<Review>().GetByIdAsync(dto.Id);
            getreview.Comment = dto.Comment;
            var result = _unitOfWork.CompleteAsync();
            return _mapper.Map<GetReviewDto>(getreview);

        }

        public async Task<IEnumerable<GetReviewDto>> GetReviewsInCourse(int courseid)
        {
            var spec = new CourseSpecificaion(courseid);
            var getcourse = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsyncWithSpec(spec);
            if(getcourse == null) return null;
            var review = getcourse.Review;
            return _mapper.Map<IEnumerable<GetReviewDto>>(review);
        }
    }
}
