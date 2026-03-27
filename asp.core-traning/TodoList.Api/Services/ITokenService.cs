using System.Security.Claims;
using TodoList.Api.Models;

namespace TodoList.Api.Services;

public interface ITokenService
{
    AuthResponse CreateTokens(string userId, string userName);
    AuthResponse? Refresh(string refreshToken);
    ClaimsPrincipal? ValidateExpiredAccessToken(string accessToken);
}
