using core_dotnet.Enums;

namespace core_dotnet.Entities
{
    public interface IBaseEntity
    {
        RecordStatus RecordStatus { get; set; }
        public string CreateUserId { get; set; }
        DateTimeOffset CreateDate { get; set; }
        public string UpdateUserId { get; set; }
        DateTimeOffset UpdateDate { get; set; }
    }
}