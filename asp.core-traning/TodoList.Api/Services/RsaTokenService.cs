using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using TodoList.Api.Models;
using TodoList.Api.Security;

namespace TodoList.Api.Services;

public class RsaTokenService : ITokenService
{
    private readonly IRsaKeyProvider _keyProvider;
    private readonly Dictionary<string, string> _refreshTokens = new(StringComparer.Ordinal);

    public RsaTokenService(IRsaKeyProvider keyProvider)
    {
        _keyProvider = keyProvider;
    }

    public AuthResponse CreateTokens(string userId, string userName)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(15);
        var accessToken = CreateAccessToken(userId, userName, expiresAtUtc);
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        _refreshTokens[refreshToken] = userId;

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    public AuthResponse? Refresh(string refreshToken)
    {
        if (!_refreshTokens.TryGetValue(refreshToken, out var userId))
        {
            return null;
        }

        _refreshTokens.Remove(refreshToken);
        return CreateTokens(userId, "henry");
    }

    public ClaimsPrincipal? ValidateExpiredAccessToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();

        using var rsa = _keyProvider.CreatePrivateRsa();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtTokenConstants.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtTokenConstants.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsa.ExportParameters(false)),
            ValidateLifetime = false
        };

        try
        {
            return handler.ValidateToken(accessToken, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    private string CreateAccessToken(string userId, string userName, DateTime expiresAtUtc)
    {
        using var rsa = _keyProvider.CreatePrivateRsa();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Name, userName)
        };

        var credentials = new SigningCredentials(
            new RsaSecurityKey(rsa.ExportParameters(true)),
            SecurityAlgorithms.RsaSha256);

        var jwt = new JwtSecurityToken(
            issuer: JwtTokenConstants.Issuer,
            audience: JwtTokenConstants.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
