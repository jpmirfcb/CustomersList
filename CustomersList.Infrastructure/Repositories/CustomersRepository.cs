using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using CustomersList.Infrastructure.Abstractions.Data.Abstract;
using CustomersList.Infrastructure.Abstractions.Data.Interfaces;

namespace CustomersList.Infrastructure.Repositories;

/// <summary>
/// Repository class for managing customers.
/// </summary>
public class CustomersRepository : RepositoryBase<Customer>, ICustomersRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersRepository"/> class.
    /// </summary>
    /// <param name="databaseConnectionFactory">The database connection factory.</param>
    public CustomersRepository( IDatabaseConnectionFactory databaseConnectionFactory ) : base(databaseConnectionFactory)
    {
    }

    /// <summary>
    /// Creates a new customer asynchronously.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>The created customer.</returns>
    public async Task<Customer> CreateAsync( Customer customer )
    {
        await ExecuteAsync("INSERT INTO Customers (Id, Name, Email, Phone) VALUES (@Id, @Name, @Email, @Phone)", customer);
        return await GetByIdAsync(customer.Id);
    }

    /// <summary>
    /// Deletes a customer asynchronously.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    public async Task DeleteAsync( Guid id )
    {
        await ExecuteAsync("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
    }

    /// <summary>
    /// Retrieves a customer by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns>The retrieved customer.</returns>
    public async Task<Customer> GetByIdAsync( Guid id )
    {
        return await QuerySingleAsync<Customer>("SELECT * FROM Customers WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    /// <summary>
    /// Retrieves a list of customers asynchronously.
    /// </summary>
    /// <param name="pageNumber">The page number of the results.</param>
    /// <param name="pageSize">The number of customers per page.</param>
    /// <returns>A tuple containing the list of customers and the total count.</returns>
    public async Task<(IEnumerable<Customer>, int)> GetListAsync( int pageNumber, int pageSize )
    {
        var customers = await QueryAsync<Customer>("SELECT * FROM Customers LIMIT @PageSize OFFSET @Offset", new { PageSize = pageSize, Offset = (pageNumber - 1) * pageSize });
        var count = await QueryEscalarAsync<int>("SELECT Count(*) From Customers");
        return (customers, count);
    }

    /// <summary>
    /// Updates a customer asynchronously.
    /// </summary>
    /// <param name="customer">The updated customer.</param>
    /// <param name="id">The ID of the customer to update.</param>
    public async Task UpdateAsync( Customer customer, Guid id )
    {
        await ExecuteAsync("UPDATE Customers SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @Id", new { customer.Name, customer.Email, customer.Phone, Id = id });
    }

    /// <summary>
    /// Checks if a customer exists asynchronously.
    /// </summary>
    /// <param name="id">The ID of the customer to check.</param>
    /// <returns>True if the customer exists, otherwise false.</returns>
    public async Task<bool> ExistsAsync( Guid id )
    {
        var customer = await QuerySingleOrDefaultAsync<Customer>("SELECT * FROM Customers WHERE Id = @Id LIMIT 1", new { Id = id });
        return customer is not null;
    }
}
