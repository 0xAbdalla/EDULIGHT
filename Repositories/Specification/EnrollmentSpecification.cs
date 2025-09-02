using EDULIGHT.Entities;

namespace EDULIGHT.Repositories.Specification
{
    public class EnrollmentSpecification : BaseSpecification<Enrollment>
    {
        public EnrollmentSpecification(int id) : base(p => p.Enrollment_Id == id)
        {
            ApplyIncludes();
        }
        public EnrollmentSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Course);
        }
    }
}
