using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly TVShowContext _context;
    private Hashtable _repositories;

    public UnitOfWork(TVShowContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }



    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(T).Name; // Name of Entity

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
