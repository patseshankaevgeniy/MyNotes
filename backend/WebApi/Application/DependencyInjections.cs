using Application.Common.Interfaces;
using Application.Common.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Services
        services
           .AddMemoryCache()
           .AddAutoMapper(Assembly.GetExecutingAssembly())
           .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
           .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services
           .AddSingleton<IGuidService, GuidService>()
           .AddSingleton<IImageUrlBuilder, ImageUrlBuilder>()
           .AddSingleton<IDateTimeService, DateTimeService>()
           .AddSingleton<IConstantsService, ConstantsService>();
            

        services
          //.AddScoped<IMessageService, MessageService>()
          .AddScoped<IUsersCacheService, UsersCacheService>();

        return services;
    }
}
