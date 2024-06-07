using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Infrastructure.Abstractions.Data.Context;
using CustomersList.Infrastructure.Abstractions.Data.Interfaces;
using CustomersList.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersList.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddSingleton<IDatabaseConnectionFactory, MySqlConnectionFactory>
            (( serviceProvider ) => new MySqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<ICustomersRepository, CustomersRepository>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        return services;
    }
}
