namespace CustomersList.Infrastructure.Abstractions.Data.Interfaces;

public interface IDatabaseContext
{
    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();

    Task<int> ExecuteAsync(string sql, object parameters = null);

    Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);

    Task<T> QuerySingleAsync<T>(string sql, object parameters = null);

    Task<T> QueryEscalarAsync<T>(string sql, object parameters = null);

    void Dispose();
}