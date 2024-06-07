using Ardalis.Result;
using CustomersList.Application.UseCases.Customers.Create;

namespace CustomersList.Application.UseCases.Interfaces;

public interface ICreateCustomer
{
    Task<Result<CreateCustomerResponse>> ExecuteAsync(CreateCustomerRequest request, CancellationToken ct);
}
