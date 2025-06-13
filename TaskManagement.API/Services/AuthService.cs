using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.API.Data;

namespace TaskManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _dbContext;

    public AuthService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<bool> SignInAsync(string username, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync("MyCookieAuth", principal);

            return true;
        }

        return false;
    }


    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync("MyCookieAuth");
    }

    public bool IsUserLoggedIn(ClaimsPrincipal user, out string? username)
    {
        username = null;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            username = user.Identity.Name;
            return true;
        }

        return false;
    }
}
