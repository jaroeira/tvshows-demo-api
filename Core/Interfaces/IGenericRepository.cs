using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity {
    Task<T> GetByIdAsync(int id);
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<int> Count(Expression<Func<T, bool>> predicate);
    Task<bool> Contains(Expression<Func<T, bool>> predicate);

}
