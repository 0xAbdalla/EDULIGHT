using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Repositories.Specification
{
    public class CartItemSpecification : BaseSpecification<CartItem>
    {
        public CartItemSpecification(int id) : base(p=>p.Id == id)
        {
            ApplyIncludes();

        }
        public CartItemSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Course);
        }
    }
}
