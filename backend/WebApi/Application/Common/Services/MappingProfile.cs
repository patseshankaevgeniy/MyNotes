using Application.UserNotes.Models;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(userModel => userModel.UserId, opt => opt.MapFrom(user => user.Id))
            .ForMember(userModel => userModel.HasMembers, opt => opt.MapFrom(user => user.GroupId.HasValue));

        CreateMap<UserNote, UserNoteModel>().ReverseMap();
    }
}