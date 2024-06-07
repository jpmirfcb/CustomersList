using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Authentication.Login;

public sealed class UserLoginMapperProfile : Profile
{
    public UserLoginMapperProfile()
    {
        CreateMap<User, UserLoginResponse>();
    }
}
