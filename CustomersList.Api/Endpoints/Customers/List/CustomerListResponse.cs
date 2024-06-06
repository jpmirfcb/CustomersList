using CustomersList.Domain.Entities;

namespace CustomersList.Api.Endpoints.Customers.List;

public class CustomerListResponse
{
    public IEnumerable<Customer> Customers { get; set; }

    public int TotalRecords { get; set; }
}
