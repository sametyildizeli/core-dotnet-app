using core_dotnet.Entities;
using core_dotnet.Repositories.Abstract;
using core_dotnet.Utilities.Paging;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace core_dotnet.Repositories.Concrete;

public abstract class EfReadRepository<T, TId> : IEfReadRepository<T, TId> where T : BaseEntity<TId>, new()
{
    protected EfReadRepository(DbContext dbContext) { Entity = dbContext.Set<T>(); }

    public DbSet<T> Entity { get; }

    public IQueryable<T> Get(Expression<Func<T, bool>> filter, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.Where(filter).AsQueryable();

        if (!tracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        return query;
    }

    public T? GetById(TId? id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        if (include != null) query = include(query);

        if (!tracking) query = query.AsNoTracking();

        return query.FirstOrDefault(p => Equals(p.Id, id));
    }

    public Task<T?> GetByIdAsync(TId id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        if (include != null) query = include(query);

        if (!tracking) query = query.AsNoTracking();

        return query.FirstOrDefaultAsync(p => Equals(p.Id, id));
    }
    public PagedResult<TDto> GetPagedResult<TDto>(int? pageNumber, int? pageSize, ICollection<Expression<Func<T, bool>>>? filters = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        if (filters != null && filters.Any()) query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        if (include != null) query = include(query);

        var skip = ((pageNumber ?? 1) - 1) * (pageSize ?? 10);

        query = query.Skip(skip).Take(pageSize ?? 10);
        query = query.AsNoTracking();
        query = query.AsSplitQuery();

        var pagedEntities = query.ProjectToType<TDto>().ToList();

        var totalCount = Count(p => p.RecordStatus == Enums.RecordStatus.Active, false);

        var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize ?? 10));

        return new PagedResult<TDto>
        {
            CurrentPage = pageNumber ?? 1,
            TotalPages = totalPages,
            Results = pagedEntities
        };
    }
    public async Task<PagedResult<TDto>> GetPagedResultAsync<TDto>(int? pageNumber, int? pageSize, ICollection<Expression<Func<T, bool>>>? filters = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        if (filters != null && filters.Any()) query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        if (include != null) query = include(query);

        var skip = ((pageNumber ?? 1) - 1) * (pageSize ?? 10);

        query = query.Skip(skip).Take(pageSize ?? 10);
        query = query.AsNoTracking();
        query = query.AsSplitQuery();

        var pagedEntities = await query.ProjectToType<TDto>().ToListAsync();

        var totalCount = await CountAsync(p => p.RecordStatus == Enums.RecordStatus.Active, false);

        var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize ?? 10));

        return new PagedResult<TDto>
        {
            CurrentPage = pageNumber ?? 1,
            TotalPages = totalPages,
            Results = pagedEntities
        };
    }

    public IEnumerable<TDto> GetChainedFilterResult<TDto>(IEnumerable<Expression<Func<T, bool>>> filters, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        if (include != null) query = include(query);

        query = query.AsNoTracking();

        var result = query.ProjectToType<TDto>().ToList();

        return result;
    }

    public async Task<IEnumerable<TDto>> GetChainedFilterResultAsync<TDto>(IEnumerable<Expression<Func<T, bool>>> filters, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = Entity.AsQueryable();

        query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        if (include != null) query = include(query);

        query = query.AsNoTracking();

        var result = await query.ProjectToType<TDto>().ToListAsync();

        return result;
    }


    public int Count(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Count();

    }

    public Task<int> CountAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.CountAsync();
    }
    public bool Any(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Any();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.AnyAsync();
    }
}