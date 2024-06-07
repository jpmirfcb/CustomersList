using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Customers.Update;

namespace CustomersList.Application.UseCases.Interfaces;

public interface IUpdateCustomer : IUseCaseCommand<UpdateCustomerRequest, UpdateCustomerRequestValidator>
{
}
