using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.API.Authentication;
using TaskManagement.API.Data;
using TaskManagement.API.DTOs;

namespace TaskManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _dbContext;
    private readonly IAuthContext _authContext;

    public AuthService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, IAuthContext authContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
        _authContext = authContext;
    }

    public async Task<bool> SignInAsync(string username, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

    public AuthUserContextDto GetUserContext()
    {
        return new AuthUserContextDto
        {
            IsAuthenticated = _authContext.IsAuthenticated,
            Username = _authContext.Username,
            UserId = _authContext.UserId
        };
    }
}
