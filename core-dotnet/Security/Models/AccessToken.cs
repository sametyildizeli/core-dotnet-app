namespace core_dotnet.Security.Models;

public class AccessToken
{
    public string Token { get; set; } = default!;
    public DateTime Expiration { get; set; }
}
