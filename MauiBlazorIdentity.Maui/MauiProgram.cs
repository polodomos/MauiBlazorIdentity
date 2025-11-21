using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MauiBlazorIdentity.Shared.Data;
using MauiBlazorIdentity.Shared.Services;
using MauiBlazorIdentity.Maui.Services;

namespace MauiBlazorIdentity.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		// Configuration de la base de données SQLite pour MAUI
		var dbPath = Path.Combine(FileSystem.AppDataDirectory, "identity.db");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlite($"Data Source={dbPath}"));

		// Configuration de l'authentification
		builder.Services.AddScoped<IAuthService, MauiAuthService>();
		builder.Services.AddScoped<MauiAuthenticationStateProvider>();
		builder.Services.AddScoped<AuthenticationStateProvider>(sp => 
			sp.GetRequiredService<MauiAuthenticationStateProvider>());
		builder.Services.AddAuthorizationCore();
		builder.Services.AddCascadingAuthenticationState();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();

		// Créer la base de données si elle n'existe pas
		using (var scope = app.Services.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			dbContext.Database.EnsureCreated();
		}

		return app;
	}
}
