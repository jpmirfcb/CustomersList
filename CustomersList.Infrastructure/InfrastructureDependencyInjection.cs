using CustomersList.Application.Repositories;
using CustomersList.Domain.Abstractions.Entities;
using CustomersList.Infrastructure.Abstractions.Data;
using CustomersList.Infrastructure.Database;
using CustomersList.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomersList.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDatabaseConnectionFactory, MySqlConnectionFactory>
            (( serviceProvider ) => new MySqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<ICustomersRepository, CustomersRepository>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        return services;
    }

    public static void InitializeDatabase(this IServiceProvider services)
    {
        var entityTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        using var scope = services.CreateScope();
        
        var factory = scope.ServiceProvider.GetRequiredService<IDatabaseConnectionFactory>();
        var context = new DatabaseContext(factory);
        var databaseInitializer = new DatabaseInitializer(context);

        databaseInitializer.InitializeAsync(entityTypes).Wait();

        return;
    }
}
