using AutoMapper;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Customers.Update;

public sealed class UpdateCustomerMapperProfile : Profile
{
    public UpdateCustomerMapperProfile()
    {
        CreateMap<UpdateCustomerRequest, Customer>();
    }
}
