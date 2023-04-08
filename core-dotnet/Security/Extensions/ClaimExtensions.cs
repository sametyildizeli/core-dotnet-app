using System.Security.Claims;

namespace core_dotnet.Security.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email) =>
        claims.Add(new Claim(IdentityModels.Email, email));

    public static void AddName(this ICollection<Claim> claims, string name) =>
        claims.Add(new Claim(IdentityModels.Name, name));

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier) =>
        claims.Add(new Claim(IdentityModels.Id, nameIdentifier));

    public static void AddRoles(this ICollection<Claim> claims, IEnumerable<string> roles)
    {
        foreach (var role in roles) claims.Add(new Claim(IdentityModels.Role, role));
    }
}