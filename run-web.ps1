# Script pour démarrer l'application Web MAUI Blazor Identity

Write-Host "=== Application MAUI Blazor Identity - Web ===" -ForegroundColor Cyan
Write-Host ""

# Nettoyer les builds précédentes
Write-Host "Nettoyage des builds précédentes..." -ForegroundColor Yellow
dotnet clean MauiBlazorIdentity.Web\MauiBlazorIdentity.Web.csproj --verbosity quiet

Write-Host "Compilation de l'application..." -ForegroundColor Yellow
dotnet build MauiBlazorIdentity.Web\MauiBlazorIdentity.Web.csproj --verbosity quiet

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "=== Démarrage de l'application Web ===" -ForegroundColor Green
    Write-Host "L'application sera disponible sur: http://localhost:5100" -ForegroundColor Green
    Write-Host ""
    Write-Host "Fonctionnalités disponibles:" -ForegroundColor Cyan
    Write-Host "  - /register  : Créer un nouveau compte" -ForegroundColor White
    Write-Host "  - /login     : Se connecter" -ForegroundColor White
    Write-Host "  - /profile   : Voir le profil utilisateur" -ForegroundColor White
    Write-Host "  - /          : Page d'accueil (protégée)" -ForegroundColor White
    Write-Host ""
    Write-Host "Appuyez sur Ctrl+C pour arrêter l'application" -ForegroundColor Yellow
    Write-Host ""
    
    dotnet run --project MauiBlazorIdentity.Web\MauiBlazorIdentity.Web.csproj --no-build
} else {
    Write-Host "Erreur lors de la compilation" -ForegroundColor Red
    exit 1
}
