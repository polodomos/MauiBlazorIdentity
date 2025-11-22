# Application MAUI Blazor Hybrid et Web avec ASP.NET Core Identity
# # .NET 10 | Blazor | MAUI | Identity | SQLite - modifié le 21 novembre 2025

Cette solution démontre une application .NET 10 complète avec authentification Identity fonctionnant à la fois sur:
- **Application Web Blazor** (navigateur)
- **Application MAUI Blazor Hybrid** (Windows, Android, iOS, macOS)

## 🏗️ Structure du Projet

```
MauiBlazorIdentity/
├── MauiBlazorIdentity.Shared/          # Bibliothèque partagée
│   ├── Models/                         # Modèles de données
│   │   └── ApplicationUser.cs          # Modèle utilisateur Identity
│   ├── Data/                           # Contexte de base de données
│   │   └── ApplicationDbContext.cs     # DbContext EF Core
│   ├── Services/                       # Interfaces de services
│   │   └── IAuthService.cs             # Interface d'authentification
│   └── Components/Pages/               # Pages Razor partagées
│       ├── Login.razor                 # Page de connexion
│       ├── Register.razor              # Page d'inscription
│       └── Profile.razor               # Page de profil utilisateur
│
├── MauiBlazorIdentity.Web/             # Application Web Blazor
│   ├── Services/
│   │   └── WebAuthService.cs           # Service d'auth pour le web
│   └── Program.cs                      # Configuration Identity Web
│
└── MauiBlazorIdentity.Maui/            # Application MAUI Hybrid
    ├── Services/
    │   ├── MauiAuthService.cs          # Service d'auth pour MAUI
    │   └── MauiAuthenticationStateProvider.cs
    └── MauiProgram.cs                  # Configuration Identity MAUI
```

## 🚀 Démarrage Rapide

### Prérequis

- .NET 10 SDK
- Visual Studio 2022 (ou VS Code avec extensions C# et .NET MAUI)
- Pour MAUI : Workloads .NET MAUI installés

### Installation des Workloads MAUI (si nécessaire)

```powershell
dotnet workload install maui
```

### Exécuter l'Application Web

```powershell
cd MauiBlazorIdentity.Web
dotnet run
```

L'application sera disponible sur `http://localhost:5000`

### Exécuter l'Application MAUI

#### Windows
```powershell
cd MauiBlazorIdentity.Maui
dotnet build -t:Run -f net10.0-windows10.0.19041.0
```

#### Android
```powershell
cd MauiBlazorIdentity.Maui
dotnet build -t:Run -f net10.0-android
```

## 🔐 Fonctionnalités d'Authentification

### Pages Disponibles

- **`/login`** - Connexion utilisateur
- **`/register`** - Inscription nouvel utilisateur
- **`/profile`** - Profil utilisateur (protégé)
- **`/`** - Page d'accueil (protégé)

### Caractéristiques

✅ **Authentification complète** avec ASP.NET Core Identity
✅ **Base de données SQLite** pour le stockage des utilisateurs
✅ **Pages partagées** entre Web et MAUI
✅ **Protection des routes** avec `[Authorize]`
✅ **Interface utilisateur responsive** avec Bootstrap
✅ **Gestion de session** persistante

## 💾 Base de Données

### Application Web
- Fichier: `identity.db` (racine du projet Web)
- Provider: SQLite via Entity Framework Core

### Application MAUI
- Fichier: `identity.db` (dans AppDataDirectory de l'appareil)
- Provider: SQLite via Entity Framework Core

**Note**: Les deux applications utilisent des bases de données séparées. Pour une synchronisation, vous devrez implémenter une API REST partagée.

## 🔧 Configuration

### Politique de Mot de Passe (modifiable dans `Program.cs`)

```csharp
options.Password.RequireDigit = true;
options.Password.RequiredLength = 6;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequireUppercase = false;
options.Password.RequireLowercase = false;
```

### Chaîne de Connexion (modifiable dans `appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=identity.db"
  }
}
```

## 📱 Plateformes Supportées

### Application Web
- ✅ Tous les navigateurs modernes (Chrome, Firefox, Edge, Safari)

### Application MAUI
- ✅ Windows 10/11 (version 1809+)
- ✅ Android 5.0+ (API 21+)
- ✅ iOS 11+
- ✅ macOS 10.15+

## 🧪 Test de l'Application

1. **Démarrer l'application** (Web ou MAUI)
2. **Accéder à `/register`** pour créer un compte
3. **Se connecter** avec les identifiants créés
4. **Accéder au profil** pour voir les informations utilisateur
5. **Se déconnecter** depuis le profil

## 🔄 Différences entre Web et MAUI

| Fonctionnalité | Web | MAUI |
|---------------|-----|------|
| Authentification | ASP.NET Core Identity complet | Authentification simplifiée avec hachage SHA256 |
| Stockage | SQLite (serveur) | SQLite (local appareil) |
| Session | Cookie serveur | Preferences API MAUI |
| AuthenticationStateProvider | Intégré Identity | Custom MauiAuthenticationStateProvider |

## 🛠️ Développement

### Ajouter une nouvelle page partagée

1. Créer le fichier `.razor` dans `MauiBlazorIdentity.Shared/Components/Pages/`
2. La page sera automatiquement disponible dans Web et MAUI

### Modifier le modèle utilisateur

1. Éditer `ApplicationUser.cs` dans le projet Shared
2. Créer une migration pour le projet Web:
   ```powershell
   cd MauiBlazorIdentity.Web
   dotnet ef migrations add NomDeLaMigration
   dotnet ef database update
   ```

## 📚 Technologies Utilisées

- **.NET 10** - Framework
- **Blazor** - UI Framework
- **MAUI** - Cross-platform framework
- **ASP.NET Core Identity** - Authentification
- **Entity Framework Core** - ORM
- **SQLite** - Base de données
- **Bootstrap 5** - CSS Framework

## 🤝 Améliorations Futures

- [ ] Synchronisation des données entre Web et MAUI via API REST
- [ ] Authentification biométrique pour MAUI
- [ ] OAuth/OpenID Connect avec fournisseurs externes (Google, Microsoft, etc.)
- [ ] Réinitialisation de mot de passe par email
- [ ] Confirmation d'email
- [ ] Authentification à deux facteurs (2FA)
- [ ] Gestion des rôles et des autorisations

## 📄 Licence

Ce projet est un exemple de démonstration pour l'apprentissage.

## 👨‍💻 Support

Pour des questions ou des problèmes, consultez la documentation officielle:
- [Documentation .NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- [Documentation Blazor](https://learn.microsoft.com/aspnet/core/blazor/)
- [Documentation ASP.NET Core Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity)
