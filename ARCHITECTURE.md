# 🏗️ Architecture de l'Application

## Vue d'Ensemble

Cette solution démontre une architecture moderne pour des applications multiplateformes avec authentification unifiée.

```
┌─────────────────────────────────────────────────────────────┐
│                    UTILISATEURS                              │
├──────────────────────┬──────────────────────────────────────┤
│   Navigateur Web     │   Application Native (Windows/       │
│   (Chrome, Edge...)  │   Android/iOS/macOS)                 │
└──────────┬───────────┴────────────┬─────────────────────────┘
           │                        │
           ▼                        ▼
┌──────────────────────┐ ┌──────────────────────────────────┐
│  MauiBlazorIdentity  │ │  MauiBlazorIdentity.Maui         │
│  .Web                │ │  (Blazor Hybrid)                 │
│  (Blazor Server)     │ │                                  │
│                      │ │                                  │
│  ┌───────────────┐   │ │  ┌───────────────┐              │
│  │ WebAuthService│   │ │  │MauiAuthService│              │
│  └───────┬───────┘   │ │  └───────┬───────┘              │
│          │           │ │          │                      │
│  ┌───────▼───────┐   │ │  ┌───────▼───────┐              │
│  │   Identity    │   │ │  │Custom Auth    │              │
│  │  (ASP.NET)    │   │ │  │ + Preferences │              │
│  └───────┬───────┘   │ │  └───────┬───────┘              │
│          │           │ │          │                      │
│  ┌───────▼───────┐   │ │  ┌───────▼───────┐              │
│  │SQLite (Web)   │   │ │  │SQLite (Local) │              │
│  │identity.db    │   │ │  │AppData        │              │
│  └───────────────┘   │ │  └───────────────┘              │
└──────────────────────┘ └──────────────────────────────────┘
           │                        │
           │                        │
           └────────┬───────────────┘
                    ▼
         ┌──────────────────────┐
         │ MauiBlazorIdentity   │
         │ .Shared              │
         │                      │
         │ • Composants Razor   │
         │ • Modèles de données │
         │ • Interfaces         │
         │ • DbContext          │
         └──────────────────────┘
```

---

## 🔧 Composants Principaux

### 1. MauiBlazorIdentity.Shared
**Rôle :** Bibliothèque partagée contenant tous les éléments communs

**Contenu :**
- **Models/** : Classes de données
  - `ApplicationUser.cs` - Modèle utilisateur Identity
  
- **Data/** : Accès aux données
  - `ApplicationDbContext.cs` - Contexte EF Core
  
- **Services/** : Interfaces de services
  - `IAuthService.cs` - Contrat d'authentification
  
- **Components/Pages/** : Pages Razor partagées
  - `Login.razor` - Page de connexion
  - `Register.razor` - Page d'inscription
  - `Profile.razor` - Page de profil

**Dépendances :**
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.AspNetCore.Components.Authorization

---

### 2. MauiBlazorIdentity.Web
**Rôle :** Application web Blazor Server

**Responsabilités :**
- Héberger l'application web
- Gérer l'authentification côté serveur avec ASP.NET Core Identity
- Fournir l'interface utilisateur dans un navigateur

**Composants clés :**
- **Services/WebAuthService.cs** : Implémentation d'`IAuthService` pour le web
  - Utilise `UserManager<ApplicationUser>`
  - Utilise `SignInManager<ApplicationUser>`
  - Gère les cookies de session
  
- **Program.cs** : Configuration
  - Identity avec Entity Framework
  - Base de données SQLite
  - Authentification et autorisation
  - Services DI

**Points d'entrée :**
- `http://localhost:5100` - Application web

---

### 3. MauiBlazorIdentity.Maui
**Rôle :** Application native multiplateforme

**Responsabilités :**
- Exécuter l'application sur Windows, Android, iOS, macOS
- Gérer l'authentification locale
- Stocker les données localement

**Composants clés :**
- **Services/MauiAuthService.cs** : Implémentation d'`IAuthService` pour MAUI
  - Gestion directe de la base de données SQLite
  - Hachage simple des mots de passe (SHA256)
  - Persistance via `Preferences.Default`
  
- **Services/MauiAuthenticationStateProvider.cs** : Fournisseur d'état d'authentification
  - Gère `AuthenticationState`
  - Crée les `ClaimsPrincipal`
  
- **MauiProgram.cs** : Configuration
  - Base de données SQLite locale
  - Services d'authentification
  - BlazorWebView

**Plateformes supportées :**
- Windows 10/11
- Android 5.0+
- iOS 11+
- macOS 10.15+

---

## 🔐 Flux d'Authentification

### Application Web

```
Utilisateur
    │
    ▼
┌─────────────┐
│ Login.razor │
└──────┬──────┘
       │ Email + Mot de passe
       ▼
┌─────────────────┐
│ WebAuthService  │
└──────┬──────────┘
       │
       ▼
┌─────────────────────┐
│ SignInManager       │
│ (ASP.NET Identity)  │
└──────┬──────────────┘
       │ Vérifie les credentials
       ▼
┌─────────────────────┐
│ SQLite Database     │
│ (identity.db)       │
└──────┬──────────────┘
       │ Utilisateur trouvé
       ▼
┌─────────────────────┐
│ Cookie de session   │
│ créé                │
└──────┬──────────────┘
       │
       ▼
   Redirigé vers /
```

### Application MAUI

```
Utilisateur
    │
    ▼
┌─────────────┐
│ Login.razor │
└──────┬──────┘
       │ Email + Mot de passe
       ▼
┌─────────────────┐
│ MauiAuthService │
└──────┬──────────┘
       │
       ▼
┌─────────────────────┐
│ SQLite Database     │
│ (AppData/identity.db)│
└──────┬──────────────┘
       │ Vérifie hash SHA256
       ▼
┌─────────────────────┐
│ Preferences.Default │
│ stocke UserId       │
└──────┬──────────────┘
       │
       ▼
┌──────────────────────────┐
│ MauiAuthenticationState  │
│ Provider                 │
└──────┬───────────────────┘
       │
       ▼
   Redirigé vers /
```

---

## 💾 Modèle de Données

### ApplicationUser

```csharp
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }      // Nom complet
    public DateTime CreatedAt { get; set; }    // Date de création
    
    // Hérité de IdentityUser :
    // - Id (string)
    // - UserName (string)
    // - Email (string)
    // - PasswordHash (string)
    // - EmailConfirmed (bool)
    // - PhoneNumber (string)
    // - etc.
}
```

### ApplicationDbContext

```csharp
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    // Tables Identity automatiques :
    // - AspNetUsers
    // - AspNetRoles
    // - AspNetUserRoles
    // - AspNetUserClaims
    // - AspNetUserLogins
    // - AspNetUserTokens
    // - AspNetRoleClaims
}
```

---

## 🔄 Cycle de Vie des Composants

### Page Protégée (avec [Authorize])

```
Utilisateur accède à /
        │
        ▼
AuthorizeRouteView
        │
        ▼
AuthenticationStateProvider
        │
        ├──► Authentifié ? ──► Page s'affiche
        │
        └──► Non authentifié ? ──► RedirectToLogin
                                         │
                                         ▼
                                    /login
```

### Inscription (Register)

```
Formulaire rempli
        │
        ▼
    Validation
        │
        ▼
IAuthService.RegisterAsync()
        │
        ├──► Web : UserManager.CreateAsync()
        │
        └──► MAUI : DbContext.Users.Add()
                │
                ▼
        Base de données mise à jour
                │
                ▼
        Redirection vers /login
```

---

## 📦 Dépendances NuGet

### Shared
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Components.Authorization" Version="10.0.0" />
```

### Web
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.0" />
```

### MAUI
```xml
<PackageReference Include="Microsoft.AspNetCore.Components.Authorization" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.0" />
```

---

## 🎯 Patterns et Pratiques

### Dependency Injection
Tous les services sont enregistrés via DI :
```csharp
builder.Services.AddScoped<IAuthService, WebAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, ...>();
```

### Repository Pattern
Le `ApplicationDbContext` agit comme un repository pour les données.

### Interface Segregation
`IAuthService` définit un contrat clair :
- `LoginAsync()`
- `RegisterAsync()`
- `LogoutAsync()`
- `GetCurrentUserAsync()`

### Component-Based Architecture
Les pages Razor sont des composants réutilisables entre Web et MAUI.

---

## 🚀 Extensibilité

### Ajouter un nouveau service
1. Créer l'interface dans `Shared/Services/`
2. Implémenter dans `Web/Services/` et `Maui/Services/`
3. Enregistrer dans `Program.cs` de chaque projet

### Ajouter une page
1. Créer le fichier `.razor` dans `Shared/Components/Pages/`
2. Ajouter `@page "/route"` en haut
3. La page est automatiquement disponible dans Web et MAUI

### Ajouter une propriété utilisateur
1. Modifier `ApplicationUser.cs`
2. Pour Web : créer une migration EF Core
3. Pour MAUI : la base sera recréée au prochain lancement

---

## 📊 Comparaison Web vs MAUI

| Aspect | Web | MAUI |
|--------|-----|------|
| **Authentification** | ASP.NET Core Identity complet | Custom simplifié |
| **Hachage mot de passe** | PBKDF2 (Identity) | SHA256 |
| **Session** | Cookie HTTP | Preferences API |
| **Base de données** | Partagée (serveur) | Locale (appareil) |
| **Sécurité** | Haute (serveur) | Moyenne (client) |
| **Performance réseau** | Requiert connexion | Fonctionne offline |

---

## 🔒 Considérations de Sécurité

### Application Web
✅ Utilise ASP.NET Core Identity (production-ready)
✅ PBKDF2 pour le hachage des mots de passe
✅ Protection CSRF intégrée
✅ Gestion des cookies sécurisée

### Application MAUI
⚠️ Hachage SHA256 simple (éducatif, pas pour la production)
⚠️ Données stockées localement sur l'appareil
⚠️ Pas de communication serveur

### Recommandations Production
1. Pour MAUI, utiliser une API REST backend
2. Implémenter OAuth 2.0 / OpenID Connect
3. Ajouter l'authentification biométrique
4. Chiffrer la base de données locale
5. Implémenter la révocation de tokens

---

**Cette architecture démontre les concepts fondamentaux pour créer des applications multiplateformes sécurisées avec .NET 10.**
