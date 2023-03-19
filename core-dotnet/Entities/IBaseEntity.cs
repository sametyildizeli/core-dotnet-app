using core_dotnet.Enums;

namespace core_dotnet.Entities
{
    public interface IBaseEntity
    {
        Status Status { get; set; }
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset UpdateDate { get; set; }
    }
}