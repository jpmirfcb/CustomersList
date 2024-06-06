using CustomersList.Application.Repositories;
using CustomersList.Domain.Entities;

namespace CustomersList.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{

    private List<User> _users;

    public UsersRepository()
    {
        _users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@domain.com",
                Password = "4dm1n",
                Active = true,
                Name = "System Administrator",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-10),
                DeactivatedAt = null
            }
        };
    }

    public Task<User> CreateAsync( User entity )
    {
        _users.Add(entity);
        return Task.FromResult(entity);
    }

    public Task DeleteAsync( Guid id )
    {
        return Task.FromResult(_users.Any(c => c.Id == id));
    }

    public Task<bool> ExistsAsync( Guid id )
    {
        return Task.FromResult(_users.Any(c => c.Id == id));
    }

    public Task<User> GetByEmailAsync( string email )
    {
        return Task.FromResult(_users.FirstOrDefault(c => c.Email == email));
    }

    public Task<User> GetByIdAsync( Guid id )
    {
        return Task.FromResult(_users.FirstOrDefault(c => c.Id == id));
    }

    public Task<(IEnumerable<User>, int)> GetListAsync( int pageNumber, int pageSize )
    {
        return Task.FromResult((_users.Skip((pageNumber - 1) * pageSize).Take(pageSize), _users.Count));
    }

    public Task UpdateAsync( User entity, Guid id )
    {
        var existingCustomer = _users.FirstOrDefault(c => c.Id == id);
        if (existingCustomer != null)
        {
            existingCustomer.Name = entity.Name;
            existingCustomer.Email = entity.Email;
        }
        return Task.CompletedTask;
    }
}
