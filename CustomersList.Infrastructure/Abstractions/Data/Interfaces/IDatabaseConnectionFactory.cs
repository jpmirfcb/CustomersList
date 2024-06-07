using System.Data.Common;

namespace CustomersList.Infrastructure.Abstractions.Data.Interfaces;

public interface IDatabaseConnectionFactory
{
    public Task<DbConnection> CreateConnectionAsync();
}
