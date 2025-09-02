using EDULIGHT.Entities;

namespace EDULIGHT.Repositories.Specification
{
    public class PaymentSpecification : BaseSpecification<Payment>
    {
        public PaymentSpecification(int id) : base(p => p.Payment_Id == id)
        {
            ApplyIncludes();
        }
        public PaymentSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Course);
        }
    }
}
