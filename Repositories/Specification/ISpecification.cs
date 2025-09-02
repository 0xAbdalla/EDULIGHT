using System.Linq.Expressions;

namespace EDULIGHT.Repositories.Specification
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public List<string> ThenInclude { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
}
