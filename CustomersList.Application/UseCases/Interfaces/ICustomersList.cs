using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Customers.List;

namespace CustomersList.Application.UseCases.Interfaces;

public interface ICustomersList : IUseCaseQuery<CustomersListRequest, CustomersListResponse, CustomersListRequestValidator>
{
}
