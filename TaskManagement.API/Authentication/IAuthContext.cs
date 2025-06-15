public interface IAuthContext
{
    int? UserId { get; }
    string? Username { get; }
    bool IsAuthenticated { get; }
}
