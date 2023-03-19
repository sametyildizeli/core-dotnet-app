using core_dotnet.Enums;

namespace core_dotnet.Entities;

public class BaseEntity<TId> : IBaseEntity
{
    public TId Id { get; set; } = default!;
    public RecordStatus RecordStatus { get; set; } = RecordStatus.Active;
    public DateTimeOffset CreateDate { get; set; }
    public string CreateUserId { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
    public string UpdateUserId { get; set; }
}