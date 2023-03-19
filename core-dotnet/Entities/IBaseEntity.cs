using core_dotnet.Enums;

namespace core_dotnet.Entities
{
    public interface IBaseEntity
    {
        RecordStatus RecordStatus { get; set; }
        string CreateUserId { get; set; }
        DateTimeOffset CreateDate { get; set; }
        string? UpdateUserId { get; set; }
        DateTimeOffset UpdateDate { get; set; }
    }
}