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
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        _logger.LogInformation("Login attempt for username: {Username}", request.Username);

        var success = await _authService.SignInAsync(request.Username, request.Password);
        if (success)
        {
            _logger.LogInformation("Login successful for user: {Username}", request.Username);
            return Ok(new { message = "Logged in" });
        }

        _logger.LogWarning("Login failed for user: {Username}", request.Username);
        return Unauthorized(new { message = "Invalid credentials" });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var username = User.Identity?.Name ?? "(unknown)";
        _logger.LogInformation("Logout requested by user: {Username}", username);

        await _authService.SignOutAsync();

        _logger.LogInformation("User {Username} logged out successfully", username);
        return Ok(new { message = "Logged out" });
    }

    [HttpGet("status")]
    public IActionResult Status()
    {
        if (_authService.IsUserLoggedIn(User, out var username))
        {
            _logger.LogInformation("Status check: user {Username} is authenticated", username);
            return Ok(new
            {
                isLoggedIn = true,
                username
            });
        }

        _logger.LogInformation("Status check: anonymous user (not authenticated)");
        return Ok(new
        {
            isLoggedIn = false
        });
    }
}
