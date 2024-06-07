using CustomersList.Domain.Entities;

namespace CustomersList.Domain.Abstractions.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync( string email );
}
