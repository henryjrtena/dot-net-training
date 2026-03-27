using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Models;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public ActionResult<AuthResponse> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var response = _tokenService.CreateTokens("1", "henry");
        return Ok(response);
    }

    [HttpPost("refresh")]
    public ActionResult<AuthResponse> Refresh(RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var response = _tokenService.Refresh(request.RefreshToken);
        return response is null ? Unauthorized() : Ok(response);
    }
}
