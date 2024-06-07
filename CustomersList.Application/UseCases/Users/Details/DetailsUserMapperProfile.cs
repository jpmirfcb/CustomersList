using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class DetailsUserMapperProfile : Profile
{
    public DetailsUserMapperProfile()
    {
        CreateMap<User, DetailsUserResponse>();
    }
}
