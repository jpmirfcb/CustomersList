using CustomersList.Domain.Entities;

namespace CustomersList.Application.Repositories;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync( string email );
}
