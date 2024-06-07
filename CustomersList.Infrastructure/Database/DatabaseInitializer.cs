using CustomersList.Domain.Attributes;
using CustomersList.Infrastructure.Abstractions.Data;
using System.Reflection;

namespace CustomersList.Infrastructure.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly DatabaseContext _context;
    private const string SCHEMA_NAME = "CustomersList";

    public DatabaseInitializer( DatabaseContext context )
    {
        _context = context;
    }

    public async Task InitializeAsync( IEnumerable<Type> entityTypes )
    {
        foreach (var entityType in entityTypes)
        {
            var createTableCommand = GenerateCreateTableCommand(entityType);
            await _context.ExecuteAsync(createTableCommand);

            var indexCommands = GenerateCreateIndexCommands(entityType);
            foreach (var indexCommand in indexCommands)
            {
                await _context.ExecuteAsync(indexCommand);
            }
        }
    }

    private async Task CreateSchemaIfNotExists()
    {
        var createSchemaCommand = $"CREATE DATABASE IF NOT EXISTS `{SCHEMA_NAME}`;";
        await _context.ExecuteAsync(createSchemaCommand);

        var useSchemaCommand = $"USE `{SCHEMA_NAME}`;";
        await _context.ExecuteAsync(useSchemaCommand);
    }

    private string GenerateCreateTableCommand( Type entityType )
    {
        var tableName = entityType.Name;
        var properties = entityType.GetProperties();

        var columns = new List<string>();
        foreach (var prop in properties)
        {
            var columnType = GetColumnType(prop.PropertyType);
            columns.Add($"{prop.Name} {columnType}");
        }

        return $"CREATE TABLE IF NOT EXISTS {tableName} ({string.Join(", ", columns)})";
    }

    private IEnumerable<string> GenerateCreateIndexCommands( Type entityType )
    {
        var tableName = entityType.Name;
        var indexProperties = entityType.GetProperties()
            .Where(p => p.GetCustomAttribute<IndexAttribute>() != null);

        foreach (var prop in indexProperties)
        {
            var indexName = $"IX_{tableName}_{prop.Name}";
            yield return $"CREATE INDEX IF NOT EXISTS {indexName} ON {tableName}({prop.Name})";
        }
    }

    private string GetColumnType( Type type )
    {
        if (type == typeof(Guid)) return "UUID";
        if (type == typeof(int)) return "INTEGER";
        if (type == typeof(string)) return "TEXT";
        if (type == typeof(DateTime)) return "DATETIME";
        if (type == typeof(bool)) return "BOOLEAN";

        throw new NotSupportedException($"Type not supported: {type.Name}");
    }
}
