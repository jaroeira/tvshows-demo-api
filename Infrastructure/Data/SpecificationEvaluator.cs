using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;

        // Evaluate where clause
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        //Evaluate order
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // Evaluate pagination
        if (spec.IsPagingEnable)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        // Evaluate joins
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
