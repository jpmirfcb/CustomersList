using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Customers.Delete;

namespace CustomersList.Application.UseCases.Interfaces;

public interface IDeleteCustomer : IUseCaseCommand<DeleteCustomerRequest, DeleteCustomerRequestValidator>
{
}
