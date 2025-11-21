using Microsoft.EntityFrameworkCore;
using MauiBlazorIdentity.Shared.Data;
using MauiBlazorIdentity.Shared.Models;
using MauiBlazorIdentity.Shared.Services;
using System.Security.Cryptography;
using System.Text;

namespace MauiBlazorIdentity.Maui.Services;

public class MauiAuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private ApplicationUser? _currentUser;

    public MauiAuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        if (user == null)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Email ou mot de passe incorrect"
            };
        }

        // Vérification simple du mot de passe (dans un vrai projet, utilisez un hash approprié)
        var hashedPassword = HashPassword(password);
        if (user.PasswordHash != hashedPassword)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Email ou mot de passe incorrect"
            };
        }

        _currentUser = user;
        
        // Sauvegarder l'état de connexion dans les préférences
        Preferences.Default.Set("UserId", user.Id);

        return new AuthResult
        {
            Success = true,
            User = user
        };
    }

    public async Task<AuthResult> RegisterAsync(string email, string password, string fullName)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (existingUser != null)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Un utilisateur avec cet email existe déjà"
            };
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = email,
            Email = email,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper(),
            FullName = fullName,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = HashPassword(password),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthResult
        {
            Success = true,
            User = user
        };
    }

    public Task LogoutAsync()
    {
        _currentUser = null;
        Preferences.Default.Remove("UserId");
        return Task.CompletedTask;
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        if (_currentUser != null)
            return _currentUser;

        var userId = Preferences.Default.Get("UserId", string.Empty);
        if (string.IsNullOrEmpty(userId))
            return null;

        _currentUser = await _context.Users.FindAsync(userId);
        return _currentUser;
    }

    // Méthode simple de hachage (pour la production, utilisez Identity avec un meilleur système)
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
