using core_dotnet.Entities;
using core_dotnet.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace core_dotnet.Repositories.Concrete;

public abstract class EfWriteRepository<T, TId> : IEFWriteRepository<T, TId> where T : BaseEntity<TId>, new()
{
    private readonly DbSet<T> _entity;
    private readonly DbContext _dbContext;

    public EfWriteRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _entity = dbContext.Set<T>();
    }

    public DbContext Context => _dbContext;

    public DbSet<T> Entity => _entity;

    public void Add(T entity) => _entity.Add(entity);

    public async Task AddAsync(T entity) => await _entity.AddAsync(entity);

    public void AddRange(IEnumerable<T> entities) => _entity.AddRange(entities);

    public Task AddRangeAsync(IEnumerable<T> entities) => _entity.AddRangeAsync(entities);
    public void Update(T entity) => _entity.Attach(entity);

    public void UpdateRange(IEnumerable<T> entities) => _entity.AttachRange(entities);

    public int ExecuteUpdate(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls) => _entity.Where(filter).ExecuteUpdate(setPropertyCalls);

    public Task<int> ExecuteUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls) => _entity.Where(filter).ExecuteUpdateAsync(setPropertyCalls);

    public int Remove(Expression<Func<T, bool>> filter) => _entity.Where(filter).ExecuteDelete();

    public Task<int> RemoveAsync(Expression<Func<T, bool>> filter) => _entity.Where(filter).ExecuteDeleteAsync();

    public int Save() => _dbContext.SaveChanges();

    public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();
}