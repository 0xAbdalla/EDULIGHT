using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Repositories.Specification
{
    public class OrderSpecificationWithPaymentIntentId : BaseSpecification<Order>
    {
        public OrderSpecificationWithPaymentIntentId(string paymentintentid) : base(o=>o.PaymentIntentId == paymentintentid)
        {
            Includes.Add(o => o.Items);
        }
    }
}
