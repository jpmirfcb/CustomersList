using CustomersList.Domain.Entities;

namespace CustomersList.Domain.Abstractions.Interfaces.Repositories;

public interface ICustomersRepository : IBaseRepository<Customer>
{
    public Task<Customer?> GetByEmailAsync( string email );
}
