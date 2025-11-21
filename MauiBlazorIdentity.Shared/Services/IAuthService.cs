using MauiBlazorIdentity.Shared.Models;

namespace MauiBlazorIdentity.Shared.Services;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task<AuthResult> RegisterAsync(string email, string password, string fullName);
    Task LogoutAsync();
    Task<ApplicationUser?> GetCurrentUserAsync();
}

public class AuthResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public ApplicationUser? User { get; set; }
}
