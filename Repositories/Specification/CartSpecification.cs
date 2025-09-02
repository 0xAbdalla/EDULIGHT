using EDULIGHT.Dto.Basket;
using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Repositories.Specification
{
    public class CartSpecification : BaseSpecification<Cart> 
    {
        public CartSpecification(int id) : base(p=>p.Id == id) 
        {
            ApplyIncludes();

        }
        public CartSpecification()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p=>p.items);
            ThenInclude.Add("items.Course.sections.ContentCourse");
        }
    }
}
