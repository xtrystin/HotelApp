using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using HAOpenIddictServer.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace HAOpenIddictServer
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("mvc") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "mvc",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "MVC client application",
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:44338/signout-callback-oidc")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:44338/signin-oidc")
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        //Permissions.Prefixes.Scope + "demo_api",
                        Permissions.Prefixes.Scope + "api1"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }

            if (await manager.FindByClientIdAsync("resource_server_1") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "resource_server_1",
                    ClientSecret = "RS1SecretIsSecretSoDoNotTell",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            // Create Scopes
            var ScopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if(await ScopeManager.FindByNameAsync("api1") == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = "api1",
                    DisplayName = "API access to the resource_server_1",
                    Resources =
                    {
                        "resource_server_1"
                    }
                };
                await ScopeManager.CreateAsync(descriptor);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
