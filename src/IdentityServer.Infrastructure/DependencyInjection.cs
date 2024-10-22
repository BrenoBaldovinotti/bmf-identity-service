using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Repositories;

namespace IdentityServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        return services;
    }
}
