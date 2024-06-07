using CustomersList.Application.Repositories;
using CustomersList.Domain.Entities;
using CustomersList.Infrastructure.Abstractions.Data;

namespace CustomersList.Infrastructure.Repositories;

/// <summary>
/// Repository class for managing users.
/// </summary>
public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersRepository"/> class.
    /// </summary>
    /// <param name="databaseConnectionFactory">The database connection factory.</param>
    public UsersRepository( IDatabaseConnectionFactory databaseConnectionFactory )
        : base(databaseConnectionFactory)
    {
    }

    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="entity">The user entity to create.</param>
    /// <returns>The created user.</returns>
    public async Task<User> CreateAsync( User entity )
    {
        await ExecuteAsync("INSERT INTO Users (Id, Name, Password, Email, Active, CreatedAt, UpdatedAt, DeactivatedAt ) VALUES (@Id, @Name, @Password, @Email, @Active, @CreatedAt, @UpdatedAt, @DeactivatedAt )", entity);
        return await GetByIdAsync(entity.Id);
    }

    /// <summary>
    /// Deletes a user asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    public async Task DeleteAsync( Guid id )
    {
        await ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
    }

    /// <summary>
    /// Checks if a user with the specified ID exists asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to check.</param>
    /// <returns>True if the user exists, otherwise false.</returns>
    public async Task<bool> ExistsAsync( Guid id )
    {
        var user = await QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id LIMIT 1", new { Id = id });
        return user is not null;
    }

    /// <summary>
    /// Gets a user by email asynchronously.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The user with the specified email, or null if not found.</returns>
    public async Task<User?> GetByEmailAsync( string email )
    {
        return await QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email LIMIT 1", new { Email = email });
    }

    /// <summary>
    /// Gets a user by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user with the specified ID.</returns>
    public async Task<User> GetByIdAsync( Guid id )
    {
        return await QuerySingleAsync<User>("SELECT * FROM Users WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    /// <summary>
    /// Gets a list of users asynchronously.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A tuple containing the list of users and the total count.</returns>
    public async Task<(IEnumerable<User>, int)> GetListAsync( int pageNumber, int pageSize )
    {
        var users = await QueryAsync<User>("SELECT * FROM Users LIMIT @PageSize OFFSET @Offset", new { PageSize = pageSize, Offset = (pageNumber - 1) * pageSize });
        var count = await QuerySingleAsync<int>("SELECT Count(*) From Users");
        return (users, count);
    }

    /// <summary>
    /// Updates a user asynchronously.
    /// </summary>
    /// <param name="entity">The updated user entity.</param>
    /// <param name="id">The ID of the user to update.</param>
    public async Task UpdateAsync( User entity, Guid id )
    {
        await ExecuteAsync("UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id", new { entity.Name, entity.Email, Id = id });
    }
}
