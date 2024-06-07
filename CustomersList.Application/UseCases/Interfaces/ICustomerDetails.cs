using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Customers.Details;

namespace CustomersList.Application.UseCases.Interfaces;

public interface ICustomerDetails : IUseCaseQuery<CustomerDetailsRequest, CustomerDetailsResponse, CustomerDetailsRequestValidator>
{
}
