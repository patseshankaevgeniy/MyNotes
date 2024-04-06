using AutoMapper;
using Domain.Entities;

namespace Application.Users.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(userModel => userModel.UserId, opt => opt.MapFrom(user => user.Id))
            .ForMember(userModel => userModel.HasMembers, opt => opt.MapFrom(user => user.GroupId.HasValue));
    }
}