using Application.Auth.Models;
using Application.Groups.Models;
using Application.Members.Models;
using Application.TelegramBot.Auth.Models;
using Application.UserNotes.Models;
using Application.Users.Models;
using AutoMapper;
using WebApi.Models.Auth;
using WebApi.Models.TelegramBot;

namespace WebApi.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LogInResultModel, LogInResultDto>().ReverseMap();

        CreateMap<UserModel, UserDto>();
           // .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom<UserImageUrlResolver>());

        CreateMap<MemberModel, MemberDto>();
        //.ForMember(dto => dto.ImageUrl, opt => opt.MapFrom<MemberImageUrlResolver>());

        CreateMap<TelegramAuthCodeModel, TelegramAuthCodeDto>()
           .ForMember(dto => dto.Link, opt => opt.MapFrom<TelegramAuthCodeLinkResolver>());


        CreateMap<UserNoteModel, UserNoteDto>().ReverseMap();

        CreateMap<GroupModel, GroupDto>();
        //CreateMap<GroupWithValueModel, GroupWithValueDto>();

    }
}