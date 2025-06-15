using System.Security.Claims;
using TaskManagement.API.DTOs;

namespace TaskManagement.Application.Services;

public interface IAuthService
{
    Task<bool> SignInAsync(string username, string password);
    Task SignOutAsync();
    AuthUserContextDto GetUserContext();
}
