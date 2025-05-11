using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionBudgétaire.Pages;
using Bunit;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Security.Claims;
using GestionBudgétaire.Shared;
using GestionBudgétaire.Components.Layout;

namespace GestionBudgetaireTest.Components
{
    public class TestMenuTests : TestContext
    {
        [Fact]
        public void NavMenu_ShouldRender_MenuItems()
        {
            // Arrange : utilisateur connecté avec rôle "Admin"
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "adminuser"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "TestAuthentication");

            var principal = new ClaimsPrincipal(identity);
            var authStateProvider = new TestAuthenticationStateProvider(new AuthenticationState(principal));
            Services.AddSingleton<AuthenticationStateProvider>(authStateProvider);

            // Act
            var cut = RenderComponent<NavMenu>();

            // Assert
            var markup = cut.Markup;
            Assert.Contains("Accueil", markup);
            Assert.Contains("Ajouter", markup);       // Pour le rôle Admin
            Assert.Contains("Déconnexion", markup);   // Si connecté
        }
    }

    // Fournisseur de contexte d'authentification factice
    public class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _authState;

        public TestAuthenticationStateProvider(AuthenticationState authState)
        {
            _authState = authState;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(_authState);
        }
    }
}
