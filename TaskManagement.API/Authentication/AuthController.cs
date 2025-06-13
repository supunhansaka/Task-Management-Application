using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services;
using TaskManagement.API.DTOs;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var success = await _authService.SignInAsync(request.Username, request.Password);
        if (success)
        {
            return Ok(new { message = "Logged in" });
        }

        return Unauthorized(new { message = "Invalid credentials" });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.SignOutAsync();
        return Ok(new { message = "Logged out" });
    }

    [HttpGet("status")]
    public IActionResult Status()
    {
        if (_authService.IsUserLoggedIn(User, out var username))
        {
            return Ok(new
            {
                isLoggedIn = true,
                username
            });
        }

        return Ok(new
        {
            isLoggedIn = false
        });
    }
}
