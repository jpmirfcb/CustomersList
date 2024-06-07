using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Users.CreateUser;

public sealed class CreateUserMapperProfile : Profile
{
    public CreateUserMapperProfile()
    {
        CreateMap<User, CreateUserRequest>();

        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<User, CreateUserResponse>();
    }
}
