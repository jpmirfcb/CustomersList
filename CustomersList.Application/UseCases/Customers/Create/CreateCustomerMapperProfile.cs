using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Customers.Create;

public sealed class CreateCustomerMapperProfile : Profile
{
    public CreateCustomerMapperProfile()
    {
        CreateMap<CreateCustomerRequest, Customer>()
            .ForMember(x => x.Id, p => p.Ignore()); 

        CreateMap<Customer, CreateCustomerResponse>();
    }
}
