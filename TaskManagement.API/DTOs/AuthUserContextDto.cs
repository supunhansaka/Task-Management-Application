namespace TaskManagement.API.DTOs;

public class AuthUserContextDto
{
    public bool IsAuthenticated { get; set; }
    public string? Username { get; set; }
    public int? UserId { get; set; }
}