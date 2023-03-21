using core_dotnet.Entities;
using core_dotnet.Repositories.Abstract;
using core_dotnet.Utilities.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Mapster;

namespace core_dotnet.Repositories.Concrete;

public abstract class EFReadRepository<T, TId> : IEFReadRepository<T, TId> where T : BaseEntity<TId>, new()
{
    private readonly DbSet<T> _entity;

    public EFReadRepository(DbContext dbContext) { _entity = dbContext.Set<T>(); }

    public DbSet<T> Entity => _entity;

    public IQueryable<T> Get(Expression<Func<T, bool>> filter, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();

        if (!tracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        return query;
    }

    public T? GetById(TId? id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (include != null) query = include(query);

        if (!tracking) query = query.AsNoTracking();

        return query.FirstOrDefault(p => Equals(p.Id, id));
    }

    public Task<T?> GetByIdAsync(TId id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (include != null) query = include(query);

        if (!tracking) query = query.AsNoTracking();

        return query.FirstOrDefaultAsync(p => Equals(p.Id, id));
    }
    public PagedResult<TDto> GetPagedResult<TDto>(int pageNumber = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (filter != null) query = query.Where(filter);

        if (include != null) query = include(query);

        int skip = (pageNumber - 1) * pageSize;
        int take = pageSize;

        query = query.Skip(skip).Take(take);
        query = query.AsNoTracking();

        var pagedEntities = query.ProjectToType<TDto>().ToList();

        int totalCount = Count(p => p.RecordStatus == Enums.RecordStatus.Active, false);

        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PagedResult<TDto>
        {
            CurrentPage = pageNumber,
            TotalPages = totalPages,
            Results = pagedEntities
        };
    }
    public async Task<PagedResult<TDto>> GetPagedResultAsync<TDto>(int pageNumber = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (filter != null) query = query.Where(filter);

        if (include != null) query = include(query);

        int skip = (pageNumber - 1) * pageSize;
        int take = pageSize;

        query = query.Skip(skip).Take(take);
        query = query.AsNoTracking();

        var pagedEntities = await query.ProjectToType<TDto>().ToListAsync();

        int totalCount = await CountAsync(p => p.RecordStatus == Enums.RecordStatus.Active, false);

        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PagedResult<TDto>
        {
            CurrentPage = pageNumber,
            TotalPages = totalPages,
            Results = pagedEntities
        };
    }

    public int Count(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Count();

    }

    public Task<int> CountAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.CountAsync();
    }
    public bool Any(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Any();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.AnyAsync();
    }
}