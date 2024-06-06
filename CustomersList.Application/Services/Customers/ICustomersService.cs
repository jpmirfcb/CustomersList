using Ardalis.Result;
using CustomersList.Application.DTO;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.Services.Customers;

public interface ICustomerService
{
    public Task<Result<(IEnumerable<Customer>, int)>> GetCustomersAsync( int pageNumber, int pageSize );

    public Task<Result<Customer>> GetCustomerAsync( Guid id );

    public Task<Result<Customer>> CreateCustomerAsync( CustomerDTO Customer );

    public Task<Result> UpdateCustomerAsync( CustomerDTO Customer, Guid id );

    public Task<Result> DeleteCustomerAsync( Guid id );

    public Task<Result> ExistsAsync( Guid id );
}
