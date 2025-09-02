using System.Linq.Expressions;

namespace EDULIGHT.Repositories.Specification
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public List<string> ThenInclude { get; set; } = new List<string>();
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification(Expression<Func<TEntity,bool>> expression)
        {
            Criteria = expression;
        }
        public BaseSpecification()
        {
            
        }
        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
