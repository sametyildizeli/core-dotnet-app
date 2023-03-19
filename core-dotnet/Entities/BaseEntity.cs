using core_dotnet.Enums;

namespace core_dotnet.Entities;

public class BaseEntity<TId> : IBaseEntity
{
    public TId Id { get; set; } = default!;
    public Status Status { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
}