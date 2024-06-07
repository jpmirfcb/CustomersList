using System.Data;
using System.Data.Common;

namespace CustomersList.Infrastructure.Abstractions.Data;

public class DatabaseContext : IDisposable, IDatabaseContext
{
    private readonly IDatabaseConnectionFactory _connectionFactory;
    private DbConnection _dbConnection;
    private DbTransaction? _dbTransaction;
    private bool _disposed;

    public DatabaseContext( IDatabaseConnectionFactory factory )
    {
        _connectionFactory = factory;
    }

    private async Task CheckConnection()
    {
        if(_dbConnection is null)
        {
            _dbConnection = await _connectionFactory.CreateConnectionAsync();
        }
    }

    public async Task BeginTransactionAsync()
    {
        await CheckConnection();
        if (_dbTransaction == null)
        {
            _dbTransaction = await _dbConnection.BeginTransactionAsync();
        }
    }

    public async Task CommitAsync()
    {
        if (_dbTransaction != null)
        {
            await _dbTransaction.CommitAsync();
            _dbTransaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_dbTransaction != null)
        {
            await _dbTransaction.RollbackAsync();
            _dbTransaction = null;
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>( string sql, object parameters = null )
    {
        await CheckConnection();

        using (var command = CreateCommand(sql, parameters))
        using (var reader = await command.ExecuteReaderAsync())
        {
            var results = new List<T>();
            while (await reader.ReadAsync())
            {
                var item = Map<T>(reader);
                results.Add(item);
            }
            return results;
        }
    }

    public async Task<T> QuerySingleAsync<T>( string sql, object parameters = null )
    {
        await CheckConnection();

        using (var command = CreateCommand(sql, parameters))
        using (var reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                return Map<T>(reader);
            }
            throw new InvalidOperationException("No rows found.");
        }
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>( string sql, object parameters = null )
    {
        await CheckConnection();

        using (var command = CreateCommand(sql, parameters))
        using (var reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                return Map<T>(reader);
            }
            return default;
        }
    }

    public async Task<int> ExecuteAsync( string sql, object parameters = null )
    {
        await CheckConnection();

        using (var command = CreateCommand(sql, parameters))
        {
            return await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<T> QueryEscalarAsync<T>( string sql, object parameters )
    {
        await CheckConnection();

        using (var command = CreateCommand(sql, parameters))
        {
            var result = await command.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

    private DbCommand CreateCommand( string sql, object parameters )
    {
        var command = _dbConnection.CreateCommand();
        command.CommandText = sql;
        command.Transaction = _dbTransaction;

        if (parameters != null)
        {
            foreach (var prop in parameters.GetType().GetProperties())
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = prop.Name;
                parameter.Value = prop.GetValue(parameters) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }

    private T Map<T>( IDataRecord record )
    {
        var obj = Activator.CreateInstance<T>();
        var type = typeof(T);

        for (int i = 0; i < record.FieldCount; i++)
        {
            var prop = type.GetProperty(record.GetName(i));
            if (prop != null && prop.CanWrite)
            {
                var value = record.GetValue(i);
                prop.SetValue(obj, value == DBNull.Value ? null : value);
            }
        }

        return obj;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose( bool disposing )
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_dbTransaction is not null)
                {
                    _dbTransaction.Rollback();
                    _dbTransaction.Dispose();
                }

                _dbConnection.Dispose();
            }
            _disposed = true;
        }
    }
}
