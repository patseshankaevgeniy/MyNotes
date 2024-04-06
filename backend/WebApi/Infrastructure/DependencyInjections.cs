using Application.Common.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
                .AddHttpContextAccessor()
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<IUserIdentityService, UserIdentityService>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<ICurrentUserInitializer>(p => (CurrentUserService)p.GetRequiredService<ICurrentUserService>());

        return services;
    }
}
