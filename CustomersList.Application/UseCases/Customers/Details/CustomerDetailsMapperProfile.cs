using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Customers.Details;

public sealed class CustomerDetailsMapperProfile : Profile
{
    public CustomerDetailsMapperProfile()
    {
        CreateMap<Customer, CustomerDetailsResponse>();
    }
}
