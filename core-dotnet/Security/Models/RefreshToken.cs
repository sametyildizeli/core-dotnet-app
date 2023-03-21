namespace core_dotnet.Security.Models;

public class RefreshToken
{
    public string? Token { get; set; }
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpiration { get; set; }
}