using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TaskManagement.API.Authentication;
public class AuthContext : IAuthContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthContext(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string? Username =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

    public int? UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(value, out var id) ? id : null;
        }
    }
}
