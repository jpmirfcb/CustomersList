using System.Data.Common;

namespace CustomersList.Infrastructure.Abstractions.Data;

public interface IDatabaseConnectionFactory
{
    public Task<DbConnection> CreateConnectionAsync();
}
