using IdentityServer.Application.Services;
using IdentityServer.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
