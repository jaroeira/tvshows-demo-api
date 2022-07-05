using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    // Filter
    Expression<Func<T, bool>> Criteria { get; }

    // Join
    List<Expression<Func<T, object>>> Includes { get; }

    // Order
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }

    // Pagination
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnable { get; }
}
