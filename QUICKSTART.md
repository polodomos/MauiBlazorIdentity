# 🚀 Guide de Démarrage Rapide

## Application MAUI Blazor Hybrid + Web avec Identity

Votre application est prête ! Voici comment la démarrer.

---

## ✅ Vérification des Prérequis

### Pour l'Application Web (obligatoire)
```powershell
dotnet --version
# Doit afficher: 10.0.100 ou supérieur
```

### Pour l'Application MAUI (optionnel)
```powershell
dotnet workload list
# Doit contenir: maui
```

Si MAUI n'est pas installé :
```powershell
dotnet workload install maui
```

---

## 🌐 Démarrer l'Application Web

### Option 1: Script PowerShell (Recommandé)
```powershell
.\run-web.ps1
```

### Option 2: Commandes manuelles
```powershell
cd MauiBlazorIdentity.Web
dotnet run
```

L'application sera disponible sur : **http://localhost:5100**

---

## 💻 Démarrer l'Application MAUI (Windows)

### Option 1: Script PowerShell (Recommandé)
```powershell
.\run-maui.ps1
```

### Option 2: Commandes manuelles
```powershell
cd MauiBlazorIdentity.Maui
dotnet build -t:Run -f net10.0-windows10.0.19041.0
```

---

## 📝 Premiers Pas

### 1. Créer un Compte
1. Lancez l'application (Web ou MAUI)
2. Accédez à `/register` ou cliquez sur "Se connecter" puis "Créer un compte"
3. Remplissez le formulaire :
   - Nom complet
   - Email
   - Mot de passe (min. 6 caractères)
4. Cliquez sur "S'inscrire"

### 2. Se Connecter
1. Accédez à `/login`
2. Entrez votre email et mot de passe
3. Cliquez sur "Se connecter"

### 3. Explorer l'Application
Une fois connecté, vous pouvez :
- ✅ Voir la page d'accueil protégée (`/`)
- ✅ Consulter votre profil (`/profile`)
- ✅ Accéder aux autres pages (Counter, Weather)
- ✅ Se déconnecter depuis le profil

---

## 🔐 Comptes de Test

Après le premier lancement, vous pouvez créer des comptes de test :

**Exemple de compte :**
- **Nom complet :** Jean Dupont
- **Email :** jean@example.com
- **Mot de passe :** Test123

---

## 📂 Structure des Bases de Données

### Application Web
- **Fichier :** `MauiBlazorIdentity.Web/identity.db`
- **Type :** SQLite
- **Créé automatiquement** au premier lancement

### Application MAUI
- **Fichier :** Dans le répertoire AppData de l'appareil
- **Type :** SQLite
- **Créé automatiquement** au premier lancement

⚠️ **Note :** Les deux applications ont des bases de données **séparées**. 
Les comptes créés dans l'application Web ne seront pas disponibles dans MAUI et vice-versa.

---

## 🛠️ Résolution des Problèmes Courants

### L'application Web ne démarre pas
```powershell
# Nettoyer et reconstruire
dotnet clean
dotnet build
dotnet run --project MauiBlazorIdentity.Web/MauiBlazorIdentity.Web.csproj
```

### Port déjà utilisé
Modifiez le port dans `MauiBlazorIdentity.Web/Properties/launchSettings.json` :
```json
"applicationUrl": "http://localhost:NOUVEAU_PORT"
```

### Erreur de compilation MAUI
```powershell
# Vérifier l'installation de MAUI
dotnet workload list

# Réinstaller si nécessaire
dotnet workload install maui
```

### La base de données n'est pas créée
La base de données est créée automatiquement au premier lancement. Si elle n'existe pas :
1. Vérifiez les permissions d'écriture dans le dossier
2. Consultez les logs dans la console

---

## 📚 Pages Disponibles

| Route | Description | Protection |
|-------|-------------|------------|
| `/` | Page d'accueil | 🔒 Protégée |
| `/login` | Connexion | 🌐 Public |
| `/register` | Inscription | 🌐 Public |
| `/profile` | Profil utilisateur | 🔒 Protégée |
| `/counter` | Compteur (demo) | 🔒 Protégée |
| `/weather` | Météo (demo) | 🔒 Protégée |

---

## 🎯 Prochaines Étapes

Maintenant que votre application fonctionne, vous pouvez :

1. **Personnaliser l'apparence**
   - Modifier les styles dans `wwwroot/app.css`
   - Ajouter votre logo

2. **Ajouter de nouvelles fonctionnalités**
   - Créer de nouvelles pages dans `Shared/Components/Pages/`
   - Ajouter des services métier

3. **Améliorer la sécurité**
   - Activer la confirmation d'email
   - Ajouter l'authentification à deux facteurs
   - Implémenter la réinitialisation de mot de passe

4. **Synchroniser les données**
   - Créer une API REST partagée
   - Utiliser SignalR pour les mises à jour en temps réel

---

## 💡 Besoin d'Aide ?

Consultez le **README.md** pour la documentation complète.

### Ressources Utiles
- [Documentation .NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- [Documentation Blazor](https://learn.microsoft.com/aspnet/core/blazor/)
- [Documentation Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity)

---

**Bon développement ! 🚀**
