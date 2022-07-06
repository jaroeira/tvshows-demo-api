using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
    private readonly TVShowContext _context;
    public GenericRepository(TVShowContext context) {
        _context = context;
    }

    public void Add(T entity) {
        _context.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec) {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate) {
        return await _context.Set<T>().Where(predicate).CountAsync();
    }

    public async Task<T> GetByIdAsync(int id) {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec) {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec) {
        return await ApplySpecification(spec).ToListAsync();
    }

    public void Remove(T entity) {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity) {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> Contains(Expression<Func<T, bool>> predicate) {
        return await Count(predicate) > 0 ? true : false;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec) {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}
