# Script pour démarrer l'application MAUI Windows
#chcp 850 > $null   # Europe occidentale ANSI
[Console]::OutputEncoding = [System.Text.Encoding]::GetEncoding(850)
Write-Host "=== Application MAUI Blazor Identity - Windows ===" -ForegroundColor Cyan
Write-Host ""

# Vérifier si MAUI est installé
$mauiInstalled = dotnet workload list | Select-String "maui"
if (-not $mauiInstalled) {
    Write-Host "Le workload .NET MAUI n'est pas installé." -ForegroundColor Red
    Write-Host "Exécutez: dotnet workload install maui" -ForegroundColor Yellow
    exit 1
}

Write-Host "Compilation de l'application MAUI..." -ForegroundColor Yellow
dotnet build MauiBlazorIdentity.Maui\MauiBlazorIdentity.Maui.csproj -f net10.0-windows10.0.19041.0 --verbosity quiet

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "=== Démarrage de l'application MAUI Windows ===" -ForegroundColor Green
    Write-Host ""
    Write-Host "Fonctionnalités disponibles:" -ForegroundColor Cyan
    Write-Host "  - /register  : Créer un nouveau compte" -ForegroundColor White
    Write-Host "  - /login     : Se connecter" -ForegroundColor White
    Write-Host "  - /profile   : Voir le profil utilisateur" -ForegroundColor White
    Write-Host "  - /          : Page d'accueil (protégée)" -ForegroundColor White
    Write-Host ""
    
    dotnet build MauiBlazorIdentity.Maui\MauiBlazorIdentity.Maui.csproj -t:Run -f net10.0-windows10.0.19041.0
} else {
    Write-Host "Erreur lors de la compilation" -ForegroundColor Red
    exit 1
}
