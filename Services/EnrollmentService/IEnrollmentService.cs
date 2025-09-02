using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Enrollment;
using EDULIGHT.Entities;

namespace EDULIGHT.Services.EnrollmentService
{
    public interface IEnrollmentService
    {
        Task<GetEnrollmentDto> AddEnrollment(Enrollment enroll);
        Task<int> GetNumberOfStudentInCourse(int courseid);
        Task<IEnumerable<GetCoursesDto>> GetMyCourseStudent(string idstudent);
        Task<GetEnrollmentDto> GetEnrollmentById(int id);
    }
}
