# GestionBudgetTest

Projet de tests unitaires pour l'application **GestionBudgétaire** en Blazor Server.

## Prérequis

- .NET 8 SDK installé (`dotnet --version`)
- Visual Studio 2022+ ou VSCode avec extension C#
- Projet principal `GestionBudgétaire` présent à la racine
- Référence de projet bien ajoutée dans `GestionBudgétaireTest.csproj`

## Structure

```
Solution/
├── GestionBudgétaire/          # Projet Blazor principal
├── GestionBudgétaireTest/      # Projet de test (ce projet)
│   ├── TestServiceTests.cs     # Tests pour les services
│   └── GestionBudgétaireTest.csproj
```

## Exécution des tests

### Avec Visual Studio :
- Ouvrir la solution `.sln`
- Aller dans **Test > Exécuter tous les tests**
- Ou ouvrir l'**Explorateur de tests**

### En ligne de commande :

```bash
cd GestionBudgétaireTest
dotnet test
```

## Technologies utilisées

- [xUnit](https://xunit.net/)
- [bUnit](https://bunit.dev/)
- `Microsoft.EntityFrameworkCore.InMemory`
- `Microsoft.NET.Test.Sdk`

## Exemple de test

```csharp
[Fact]
public async Task AddDialogueAsync_ShouldAddDialogue()
{
    var context = await GetInMemoryDbContextAsync();
    var service = new TestService(context);

    var dialogue = new Dialogue { DG_CODE = 1, COMMENTAIRES = "Test" };
    await service.AddDialogueAsync(dialogue);

    var result = await context.Dialogue.FindAsync(1);
    Assert.NotNull(result);
    Assert.Equal("Test", result!.COMMENTAIRES);
}
```

## Remarques

- Tous les tests utilisent une base de données **InMemory**.
- Chaque test recrée une base isolée pour garantir l’indépendance des résultats.

## Références
- [Source 1](https://blog.openreplay.com/how-to--unit-testing-blazor-apps/)
- [Test Razor components in ASP.NET Core Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-9.0)
