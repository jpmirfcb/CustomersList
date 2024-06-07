using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Customers.List;

public sealed class CustomersListMapperProfile : Profile
{
    public CustomersListMapperProfile()
    {
        CreateMap<(IEnumerable<Customer>, int), CustomersListResponse>()
            .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.TotalRecords, opt => opt.MapFrom(src => src.Item2));
    }
}
