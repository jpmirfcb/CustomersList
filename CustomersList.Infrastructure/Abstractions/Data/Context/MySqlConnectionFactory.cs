using CustomersList.Infrastructure.Abstractions.Data.Interfaces;
using MySqlConnector;
using System.Data.Common;

namespace CustomersList.Infrastructure.Abstractions.Data.Context;

public class MySqlConnectionFactory : IDatabaseConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
