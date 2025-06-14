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


    [HttpGet("context")]
    public IActionResult GetUserContext()
    {
        try
        {
            var context = _authService.GetUserContext();

            if (context.IsAuthenticated)
            {
                _logger.LogInformation("User context retrieved: {Username} (ID: {UserId})", context.Username, context.UserId);
            }
            else
            {
                _logger.LogInformation("Anonymous user requested context.");
            }

            return Ok(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while retrieving user context.");
            return StatusCode(500, new { message = "Failed to retrieve user context." });
        }
    }
}
