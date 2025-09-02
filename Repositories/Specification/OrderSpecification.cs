using EDULIGHT.Entities.Enroll_Pay;

namespace EDULIGHT.Repositories.Specification
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string buyerEmail, int orderid)
            :base(o=>o.BuyerEmail == buyerEmail && o.OrderId == orderid)
        {
            Includes.Add(o => o.Items);
        }
        public OrderSpecification(string buyerEmail)
            : base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.Items);
        }
    }
}
