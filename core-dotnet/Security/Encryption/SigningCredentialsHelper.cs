using Microsoft.IdentityModel.Tokens;

namespace core_dotnet.Security.Encryption;

public static class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) => new(securityKey, SecurityAlgorithms.HmacSha512Signature);
}
