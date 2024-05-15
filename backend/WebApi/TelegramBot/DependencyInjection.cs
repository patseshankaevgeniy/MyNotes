using Application.TelegramBot.Common.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Handlers;
using TelegramBot.MessageSender;
using TelegramBot.Options;
using TelegramBot.Recevier;

namespace TelegramBot;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBotDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services
            .AddSingleton<IUpdateReceiver, UpdateReceiver>();

        if (configuration.GetValue<bool?>("TelegramBot:IsActive").Value == false)
        {
            services
                .AddHostedService<TelegramWorkerService>();
        }

        services
            .AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(configuration["TelegramToken"], httpClient));

        services
                .AddOptions<TelegraBotOptions>()
                .Bind(configuration.GetSection(TelegraBotOptions.SectionName))
                .Validate(options => !string.IsNullOrEmpty(options.LinkTemplate));

        services
           .AddTransient<ITelegramMessageSender, TelegramMessageSender>();

        services
            .AddScoped<IUpdateHandlerFactory, UpdateHandlerFactory>()
            .AddTransient<IUpdateHandler, UnkmownUpdateHandler>()
            .AddTransient<IUpdateHandler, MessageUpdateHandler>();

        return services;
    }
}