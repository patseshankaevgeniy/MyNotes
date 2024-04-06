﻿using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using WebApi.Models.Auth;

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

//public sealed class MemberImageUrlResolver : IValueResolver<MemberModel, MemberDto, string>
//{
//    private readonly IImageUrlBuilder _imageUrlBuilder;

//    public MemberImageUrlResolver(IImageUrlBuilder imageUrlBuilder)
//    {
//        _imageUrlBuilder = imageUrlBuilder;
//    }

//    public string Resolve(MemberModel source, MemberDto ___, string __, ResolutionContext _)
//    {
//        return _imageUrlBuilder.Build(source.ImageId.ToString());
//    }
//}

//public sealed class TelegramAuthCodeLinkResolver : IValueResolver<TelegramAuthCodeModel, TelegramAuthCodeDto, string>
//{
//    private readonly TelegraBotOptions _telegramBotOptions;
//    private readonly ITelegramBotClient _telegramBotClient;

//    public TelegramAuthCodeLinkResolver(
//        IOptions<TelegraBotOptions> options,
//        ITelegramBotClient telegramBotClient)
//    {
//        _telegramBotOptions = options.Value;
//        _telegramBotClient = telegramBotClient;
//    }

//public string Resolve(TelegramAuthCodeModel source, TelegramAuthCodeDto ___, string __, ResolutionContext _)
//{
//    var bot = _telegramBotClient.GetMeAsync().GetAwaiter().GetResult();
//    return string.Format(_telegramBotOptions.LinkTemplate, bot.Username, source.LinkCode);
//}