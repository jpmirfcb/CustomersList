using CustomersList.Application.Repositories;
using CustomersList.Domain.Entities;

namespace CustomersList.Infrastructure.Repositories;

public class CustomersRepository : ICustomersRepository
{

    private List<Customer> _list;

    public CustomersRepository()
    {
        _list = new List<Customer>()
        {
            new Customer() { Id = Guid.Parse("e56e14b5-af6b-4fad-bb44-27da64136c65"), Name = "Sally", Email = "sally@company.com"},
            new Customer() { Id = Guid.Parse("abef3e2d-39f1-4885-8e49-eea28074efb1"), Name = "Peter", Email = "peter@company.com"},
            new Customer() { Id = Guid.Parse("4e08e02a-e4b5-4206-aa7d-88824bd19729"), Name = "Robert", Email = "bob@company.com"}
        };
    }
    public Task<Customer> CreateAsync( Customer customer )
    {
        _list.Add(customer);
        return Task.FromResult(customer);
    }

    public Task DeleteAsync( Guid id )
    {
        _list.RemoveAll(c => c.Id == id);
        return Task.CompletedTask;
    }

    public Task<Customer> GetByIdAsync( Guid id )
    {
        return Task.FromResult(_list.FirstOrDefault(c => c.Id == id));
    }

    public Task<(IEnumerable<Customer>, int)> GetListAsync( int pageNumber, int pageSize )
    {
        return Task.FromResult((_list.Skip((pageNumber-1) * pageSize).Take(pageSize), _list.Count));
    }

    public Task UpdateAsync( Customer customer, Guid id )
    {
        var existingCustomer = _list.FirstOrDefault(c => c.Id == id);
        if(existingCustomer != null)
        {
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_list.Any(c => c.Id == id));
    }
}
