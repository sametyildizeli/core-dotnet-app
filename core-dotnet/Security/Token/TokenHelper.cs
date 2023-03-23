using core_dotnet.Entities;
using core_dotnet.Security.Encryption;
using core_dotnet.Security.Extensions;
using core_dotnet.Security.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace core_dotnet.Security.Token;

public class TokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    public TokenHelper(IOptions<TokenOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions.Value;
    }

    public AccessToken CreateToken(IBaseUserEntity user, IEnumerable<string> userRoles)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, userRoles);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        var refreshToken = GenerateRefreshToken();

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration,
            RefreshToken = refreshToken
        };
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, IBaseUserEntity user, SigningCredentials signingCredentials, IEnumerable<string> roles) =>
        new(issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: SetClaims(user, roles),
            signingCredentials: signingCredentials);

    private static IEnumerable<Claim> SetClaims(IBaseUserEntity user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.UserId);
        claims.AddName($"{user.Name} {user.Surname}");
        claims.AddEmail(user.Email);
        claims.AddRoles(roles);
        return claims;
    }

    private RefreshToken GenerateRefreshToken()
    {
        var currentUtc = DateTime.UtcNow;
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            TokenCreated = currentUtc,
            TokenExpiration = currentUtc.AddDays(_tokenOptions.RefreshTokenExpiration)
        };

        return refreshToken;
    }
}