using CustomersList.Domain.Entities;

namespace CustomersList.Application.UseCases.Customers.List;

public sealed class CustomersListResponse
{
    public IEnumerable<Customer> Customers { get; set; }

    public int TotalRecords { get; set; }
}
