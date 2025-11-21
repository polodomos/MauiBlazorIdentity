using Microsoft.AspNetCore.Components.Authorization;
using MauiBlazorIdentity.Shared.Services;
using System.Security.Claims;

namespace MauiBlazorIdentity.Maui.Services;

public class MauiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;

    public MauiAuthenticationStateProvider(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _authService.GetCurrentUserAsync();

        if (user != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "MauiAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(claimsPrincipal);
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
