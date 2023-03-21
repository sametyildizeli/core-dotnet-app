using core_dotnet.Entities;
using core_dotnet.Utilities.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace core_dotnet.Repositories.Abstract;

public interface IEFReadRepository<T, TId> : IEFRepository<T, TId> where T : BaseEntity<TId>, new()
{
    IQueryable<T> Get(Expression<Func<T, bool>> filter, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    T? GetById(TId id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<T?> GetByIdAsync(TId id, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    PagedResult<TDto> GetPagedResult<TDto>(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<PagedResult<TDto>> GetPagedResultAsync<TDto>(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    bool Any(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true);
    int Count(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<int> CountAsync(Expression<Func<T, bool>> filter, bool tracking = true);
}