using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class UserDetailsMapperProfile : Profile
{
    public UserDetailsMapperProfile()
    {
        CreateMap<User, UserDetailsResponse>();
    }
}
