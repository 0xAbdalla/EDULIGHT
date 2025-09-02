using AutoMapper;
using EDULIGHT.Dto.Basket;
using EDULIGHT.Dto.Cart;
using EDULIGHT.Dto.Category;
using EDULIGHT.Dto.ContentCourse;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Enrollment;
using EDULIGHT.Dto.Order;
using EDULIGHT.Dto.Payment;
using EDULIGHT.Dto.Review;
using EDULIGHT.Dto.Roadmap;
using EDULIGHT.Dto.Section;
using EDULIGHT.Entities;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Mapping
{
    public class AppProfile : Profile
    {
        public AppProfile() 
        {
            CreateMap<Category, GetCategoryDto>().ForMember(p=>p.PosterURL,options=>options.MapFrom(s=>$"http://edulightapi.runasp.net/{s.PosterURL}"));
            CreateMap<Category, PostCategoryDto>();
            CreateMap<ContentCourse, GetContentCourseDto>().ForMember(p=>p.ContentUrl,opt=>opt.MapFrom(s=>$"http://edulightapi.runasp.net/{s.ContentUrl}"));
            CreateMap<ContentCourse, PostContentCourseDto>();
            CreateMap<Course, GetCoursesDto>().ForMember(p=>p.PosterUrl,options=>options.MapFrom(s => $"http://edulightapi.runasp.net/{s.PosterURL}")).ForMember(p=>p.Level,options=>options.MapFrom(s=>$"{s.Level.ToString()}")).ForMember(p => p.Language, options => options.MapFrom(s => $"{s.Language.ToString()}"));
            //CreateMap<IEnumerable<Course>, IEnumerable<GetCoursesDto>>();//.ForMember(p => p.PosterUrl, options => options.MapFrom(s => $"https://localhost:7220/{s.PosterURL}")).ForMember(p => p.Level, options => options.MapFrom(s => $"{s.Level.ToString()}")).ForMember(p => p.Language, options => options.MapFrom(s => $"{s.Language.ToString()}"));

            CreateMap<Course, GetFiltercourseDto>();
            CreateMap<Course, PostCoursesDto>();
            CreateMap<Sections, sectionDto>().ReverseMap();
            CreateMap<Sections, ReturnSectionDto>().ReverseMap();
            CreateMap<Roadmap, GetRoadmapDto>().ForMember(p => p.PosterURL, options => options.MapFrom(s => $"http://edulightapi.runasp.net/{s.PosterURL}"));
            CreateMap<Roadmap, PostRoadmapDto>();
            CreateMap<Enrollment, GetEnrollmentDto>().ReverseMap();
            CreateMap<Enrollment, PostEnrollmentDto>().ReverseMap();
            CreateMap<Payment, GetPaymentDto>();
            CreateMap<Payment, PostPaymentDto>();
            CreateMap<Cart,ReturnCartDto>().ReverseMap();
            CreateMap<Cart,CartDto>().ReverseMap();
            CreateMap<CartItem, PostCartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();

            CreateMap<Review, PostReviewDto>().ReverseMap();
            CreateMap<Review, EditReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewDto>().ReverseMap();
            CreateMap<Order,OrderToReturnDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o=>o.CourseId,op=>op.MapFrom(o=>o.Course.CourseId))
                .ForMember(o => o.CourseName, op => op.MapFrom(o => o.Course.CourseName))
                .ForMember(o => o.PictureUrl, op => op.MapFrom(o => $"http://edulightapi.runasp.net/{o.Course.PictureUrl}"))
            ;

        }
    }
}
