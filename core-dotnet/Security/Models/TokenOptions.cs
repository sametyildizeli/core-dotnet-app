namespace core_dotnet.Security.Models;

public class TokenOptions
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public int AccessTokenExpiration { get; set; } = default!;
    public int RefreshTokenExpiration { get; set; } = default!;
    public string SecurityKey { get; set; } = default!;
}
