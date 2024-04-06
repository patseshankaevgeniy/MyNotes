using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Options;
using SignalR.Services;

namespace SignalR;

public static class DependencyInjection
{
    public static IServiceCollection AddSignalRDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<SignalROptions>()
            .Bind(configuration.GetSection(SignalROptions.SectionName))
            .Validate(options => !string.IsNullOrEmpty(options.FrontendUrl))
            .Validate(options => !string.IsNullOrEmpty(options.NotificationMethodName));

        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }
}