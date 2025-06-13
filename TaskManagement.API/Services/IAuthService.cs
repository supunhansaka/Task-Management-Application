using System.Security.Claims;

namespace TaskManagement.Application.Services;

public interface IAuthService
{
    Task<bool> SignInAsync(string username, string password);
    Task SignOutAsync();
    bool IsUserLoggedIn(ClaimsPrincipal user, out string? username);
}
