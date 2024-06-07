using CustomersList.Domain.Abstractions.Entities;

namespace CustomersList.Domain.Abstractions.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class, IEntity
{
    public Task<(IEnumerable<T>, int)> GetListAsync( int pageNumber, int pageSize );

    public Task<T> GetByIdAsync( Guid id );

    public Task<T> CreateAsync( T entity );

    public Task UpdateAsync( T entity, Guid id );

    public Task DeleteAsync( Guid id );
    public Task<bool> ExistsAsync( Guid id );
}
