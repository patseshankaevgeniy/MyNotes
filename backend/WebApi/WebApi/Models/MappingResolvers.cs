using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using Application.Users.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramBot.Options;
using WebApi.Models.Auth;
using WebApi.Models.TelegramBot;

namespace WebApi.Models;

public sealed class UserImageUrlResolver : IValueResolver<UserModel, UserDto, string>
{
    private readonly IImageUrlBuilder _imageUrlBuilder;

    public UserImageUrlResolver(IImageUrlBuilder imageUrlBuilder)
    {
        _imageUrlBuilder = imageUrlBuilder;
    }

    public string Resolve(UserModel source, UserDto ___, string __, ResolutionContext _)
    {
        return _imageUrlBuilder.Build(source.ImageId.ToString());
    }
}

public sealed class TelegramAuthCodeLinkResolver : IValueResolver<TelegramAuthCodeModel, TelegramAuthCodeDto, string>
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly TelegraBotOptions _telegramBotOptions;

    public TelegramAuthCodeLinkResolver(
        ITelegramBotClient telegramBotClient,
        IOptions<TelegraBotOptions> options)
    {
        _telegramBotClient = telegramBotClient;
        _telegramBotOptions = options.Value;
    }

    public string Resolve(TelegramAuthCodeModel source, TelegramAuthCodeDto ___, string __, ResolutionContext _)
    {
        var bot = _telegramBotClient.GetMeAsync().GetAwaiter().GetResult();
        return string.Format(_telegramBotOptions.LinkTemplate, bot.Username, source.LinkCode);
    }
}