using AutoMapper;
using EDULIGHT.Configrations;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Enrollment;
using EDULIGHT.Entities;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using Microsoft.EntityFrameworkCore;

namespace EDULIGHT.Services.EnrollmentService
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly EdulightDbContext _context;

        public EnrollmentService(IUnitOfWork unitOfWork,IMapper mapper,EdulightDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        public async Task<GetEnrollmentDto> AddEnrollment(Enrollment enroll)
        {
            if (enroll == null) return null;
            var spec = new EnrollmentSpecification(enroll.Enrollment_Id);
            var getenroll = await _unitOfWork.GetGenericRepositories<Enrollment>().GetByIdAsyncWithSpec(spec);
            if (getenroll != null) return null;
            var addenroll = await _unitOfWork.GetGenericRepositories<Enrollment>().AddAsync(enroll);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetEnrollmentDto>(addenroll);
        }

        public async Task<GetEnrollmentDto> GetEnrollmentById(int id)
        {
            if (id == 0) return null;
            var spec = new EnrollmentSpecification(id);
            var getenroll = await _unitOfWork.GetGenericRepositories<Enrollment>().GetByIdAsyncWithSpec(spec);
            if (getenroll == null) return null;
            return _mapper.Map<GetEnrollmentDto>(getenroll);
        }

        public async Task<IEnumerable<GetCoursesDto>> GetMyCourseStudent(string idstudent)
        {
            if(string.IsNullOrEmpty(idstudent)) return null;
            var getmycourses = _context.Enrollments.Include(p=>p.Course).Where(p=>p.Student_id == idstudent).Select(p => p.Course).ToList();
            if(getmycourses == null) return null;
            return _mapper.Map<IEnumerable<GetCoursesDto>>(getmycourses);
        }

        public async Task<int> GetNumberOfStudentInCourse(int courseid)
        {
            if (courseid == 0) return 0;
            var getmyenstudents = _context.Enrollments.Where(p => p.course_id == courseid).ToList();
            if (getmyenstudents.Count() == 0) return 0;
            return getmyenstudents.Count();
        }
    }
}
