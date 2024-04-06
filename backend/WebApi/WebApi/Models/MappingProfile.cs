using Application.Auth.Models;
using Application.Users.Models;
using AutoMapper;
using WebApi.Models.Auth;

namespace WebApi.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LogInResultModel, LogInResultDto>().ReverseMap();

        CreateMap<UserModel, UserDto>()
            .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom<UserImageUrlResolver>());

        //CreateMap<MemberModel, MemberDto>()
        //    .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom<MemberImageUrlResolver>());

        //CreateMap<TelegramAuthCodeModel, TelegramAuthCodeDto>()
        //    .ForMember(dto => dto.Link, opt => opt.MapFrom<TelegramAuthCodeLinkResolver>());

        //CreateMap<TelegramBotConfigurationModel, TelegramBotConfigurationDto>();


        //CreateMap<GroupModel, GroupDto>();
        //CreateMap<GroupWithValueModel, GroupWithValueDto>();

    }
}