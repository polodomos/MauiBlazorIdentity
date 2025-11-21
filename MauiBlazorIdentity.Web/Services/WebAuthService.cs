using Microsoft.AspNetCore.Identity;
using MauiBlazorIdentity.Shared.Models;
using MauiBlazorIdentity.Shared.Services;

namespace MauiBlazorIdentity.Web.Services;

public class WebAuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public WebAuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Email ou mot de passe incorrect"
            };
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
        
        return new AuthResult
        {
            Success = result.Succeeded,
            ErrorMessage = result.Succeeded ? null : "Email ou mot de passe incorrect",
            User = result.Succeeded ? user : null
        };
    }

    public async Task<AuthResult> RegisterAsync(string email, string password, string fullName)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            return new AuthResult
            {
                Success = true,
                User = user
            };
        }

        return new AuthResult
        {
            Success = false,
            ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description))
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);
        return user;
    }
}
