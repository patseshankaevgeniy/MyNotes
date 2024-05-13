using Application.TelegramBot.Auth.Models;
using AutoMapper;
using Domain.Entities.NewFolder;

namespace Application.TelegramBot.TelegramUser.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.NewFolder.TelegramUser, TelegramUserModel>();
        CreateMap<TelegramAuthCode, TelegramAuthCodeModel>();
    }
}