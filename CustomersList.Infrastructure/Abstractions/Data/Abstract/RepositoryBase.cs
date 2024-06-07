using CustomersList.Domain.Abstractions.Entities;
using CustomersList.Infrastructure.Abstractions.Data.Context;
using CustomersList.Infrastructure.Abstractions.Data.Interfaces;

namespace CustomersList.Infrastructure.Abstractions.Data.Abstract;

/// <summary>
/// Base class for repositories that provides common functionality for interacting with the database.
/// </summary>
public abstract class RepositoryBase<TEntity> where TEntity : IEntity
{
    private DatabaseContext? _context = null;
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
    private bool _disposed;

    public RepositoryBase(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    /// <summary>
    /// Gets or sets the database context.
    /// </summary>
    protected DatabaseContext Context
    {
        get
        {
            if (_context == null)
            {
                _context = new DatabaseContext(_databaseConnectionFactory);
            }
            return _context;
        }
        set
        {
            _context = value;
        }
    }

    /// <summary>
    /// Sets an external context for the repository.
    /// </summary>
    /// <param name="externalContext">The external database context.</param>
    public virtual void SetExternalContext(DatabaseContext externalContext)
    {
        _context = externalContext ?? throw new ArgumentNullException(nameof(externalContext));
    }

    /// <summary>
    /// Executes a query asynchronously and returns the result as an IEnumerable of TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="sql">The SQL query.</param>
    /// <param name="parameters">The query parameters.</param>
    /// <returns>A task representing the asynchronous operation with the result as an IEnumerable of TEntity.</returns>
    public virtual Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object parameters = null)
    {
        return Context.QueryAsync<TEntity>(sql, parameters);
    }

    /// <summary>
    /// Executes a query asynchronously and returns a single result of TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="sql">The SQL query.</param>
    /// <param name="parameters">The query parameters.</param>
    /// <returns>A task representing the asynchronous operation with the result as a single TEntity.</returns>
    public virtual Task<TEntity> QuerySingleAsync<TEntity>(string sql, object parameters = null)
    {
        return Context.QuerySingleAsync<TEntity>(sql, parameters);
    }

    /// <summary>
    /// Executes a query asynchronously and returns a single result of TEntity or null if no result is found.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="sql">The SQL query.</param>
    /// <param name="parameters">The query parameters.</param>
    /// <returns>A task representing the asynchronous operation with the result as a single TEntity or null.</returns>
    public virtual Task<TEntity?> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null)
    {
        return Context.QuerySingleOrDefaultAsync<TEntity>(sql, parameters);
    }

    /// <summary>
    /// Executes a non-query SQL statement asynchronously and returns the number of affected rows.
    /// </summary>
    /// <param name="sql">The SQL statement.</param>
    /// <param name="parameters">The statement parameters.</param>
    /// <returns>A task representing the asynchronous operation with the number of affected rows.</returns>
    public virtual Task<int> ExecuteAsync(string sql, object parameters = null)
    {
        return Context.ExecuteAsync(sql, parameters);
    }

    /// <summary>
    /// Executes a query asynchronously and returns a single scalar value of type T.
    /// </summary>
    /// <typeparam name="T">The type of the scalar value.</typeparam>
    /// <param name="sql">The SQL query.</param>
    /// <param name="parameters">The query parameters.</param>
    /// <returns>A task representing the asynchronous operation with the result as a single scalar value of type T.</returns>
    public virtual Task<T> QueryEscalarAsync<T>(string sql, object parameters = null)
    {
        return Context.QueryEscalarAsync<T>(sql, parameters);
    }

    /// <summary>
    /// Disposes of the repository and releases any resources used.
    /// </summary>
    /// <param name="disposing">A boolean indicating whether the method is called from the Dispose method.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// Disposes of the repository and releases any resources used.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
