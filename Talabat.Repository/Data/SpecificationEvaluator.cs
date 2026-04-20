using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data;

public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
{

    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery; // _context.Set<TEntity> 

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.IsPaginationEnable == true)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDesc != null)
        {
            query = query.OrderByDescending(spec.OrderByDesc);
        }

        query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
        
        return query;
    }
}