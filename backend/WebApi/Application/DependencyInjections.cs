using Application.Common.Interfaces;
using Application.Common.MediatRPipeline;
using Application.Common.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services
            .AddMemoryCache()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services
           .AddSingleton<IGuidService, GuidService>()
           .AddSingleton<IImageUrlBuilder, ImageUrlBuilder>()
           .AddSingleton<IDateTimeService, DateTimeService>()
           .AddSingleton<IConstantsService, ConstantsService>();


        services
          .AddScoped<IMessageService, MessageService>()
          .AddScoped<IUsersCacheService, UsersCacheService>();

        return services;
    }
}
