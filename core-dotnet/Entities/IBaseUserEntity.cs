namespace core_dotnet.Entities;

public interface IBaseUserEntity
{
    string UserId { get; }
    string Name { get; set; }
    string Surname { get; set; }
    string Email { get; set; }
}