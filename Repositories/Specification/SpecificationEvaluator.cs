using Microsoft.EntityFrameworkCore;

namespace EDULIGHT.Repositories.Specification
{
    public static class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) 
        {
            var query = inputQuery;
            if (spec.Criteria != null) 
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (currentquery, includeexpression) => currentquery.Include(includeexpression));
            query = spec.ThenInclude.Aggregate(query, (currentquery, includeexpression) => currentquery.Include(includeexpression));

            return query;

        }
    }
}
