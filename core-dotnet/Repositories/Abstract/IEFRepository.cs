using core_dotnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace core_dotnet.Repositories.Abstract;

public interface IEFRepository<T, TId> where T : BaseEntity<TId>, new()
{
    DbSet<T> Entity { get; }
}