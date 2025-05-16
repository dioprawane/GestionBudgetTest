using Bunit;
using GestionBudgétaire.Components.Layout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Xunit;

namespace GestionBudgetaireTest.Components
{
    public class TestMenuTests : TestContext
    {
        public TestMenuTests()
        {
            // Authentification simulée
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "adminuser"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "TestAuth"));

            var authState = new AuthenticationState(user);
            Services.AddSingleton<AuthenticationStateProvider>(new TestAuthProvider(authState));

            // Autorisation factice (toutes les policies autorisées)
            Services.AddAuthorization(options => { });
            Services.AddSingleton<IAuthorizationHandler, PassThroughAuthorizationHandler>();
            Services.AddSingleton<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();
        }

        /*[Fact]
        public void NavMenu_ShouldRender_MenuItems()
        {
            // Rendu avec contexte d’authentification
            var cut = RenderComponent<CascadingAuthenticationState>(parameters => parameters
                .AddChildContent<NavMenu>());

            var markup = cut.Markup;
            Assert.Contains("Accueil", markup);
            Assert.Contains("Ajouter", markup); // menu réservé aux Admins
        }*/
        private class TestAuthProvider : AuthenticationStateProvider
        {
            private readonly AuthenticationState _authState;

            public TestAuthProvider(AuthenticationState authState) => _authState = authState;
            public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(_authState);
        }

        private class PassThroughAuthorizationHandler : IAuthorizationHandler
        {
            public Task HandleAsync(AuthorizationHandlerContext context)
            {
                foreach (var requirement in context.PendingRequirements.ToList())
                    context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }
    }
}
