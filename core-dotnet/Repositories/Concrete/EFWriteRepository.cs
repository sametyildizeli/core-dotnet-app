using core_dotnet.Entities;
using core_dotnet.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace core_dotnet.Repositories.Concrete;

public abstract class EfWriteRepository<T, TId> : IEfWriteRepository<T, TId> where T : BaseEntity<TId>, new()
{
    protected EfWriteRepository(DbContext dbContext)
    {
        Context = dbContext;
        Entity = dbContext.Set<T>();
    }

    public DbContext Context { get; }

    public DbSet<T> Entity { get; }

    public void Add(T entity) => Entity.Add(entity);

    public async Task AddAsync(T entity) => await Entity.AddAsync(entity);

    public void AddRange(IEnumerable<T> entities) => Entity.AddRange(entities);

    public Task AddRangeAsync(IEnumerable<T> entities) => Entity.AddRangeAsync(entities);
    public void Update(T entity) => Entity.Attach(entity);

    public void UpdateRange(IEnumerable<T> entities) => Entity.AttachRange(entities);

    public int ExecuteUpdate(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls) => Entity.Where(filter).ExecuteUpdate(setPropertyCalls);

    public Task<int> ExecuteUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls) => Entity.Where(filter).ExecuteUpdateAsync(setPropertyCalls);

    public int Remove(Expression<Func<T, bool>> filter) => Entity.Where(filter).ExecuteDelete();

    public Task<int> RemoveAsync(Expression<Func<T, bool>> filter) => Entity.Where(filter).ExecuteDeleteAsync();

    public int Save() => Context.SaveChanges();

    public Task<int> SaveAsync() => Context.SaveChangesAsync();
}