using AutoMapper;
using CustomersList.Application.UseCases.Authentication.Login;
using CustomersList.Application.UseCases.Customers.Create;
using CustomersList.Application.UseCases.Customers.Delete;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Application.UseCases.Customers.List;
using CustomersList.Application.UseCases.Customers.Update;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Application.UseCases.Users.CreateUser;
using CustomersList.Application.UseCases.Users.Details;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersList.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication( this IServiceCollection services )
    {
        services.AddAutoMapper(typeof(ApplicationDependencyInjection));

        var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ApplicationDependencyInjection).Assembly));


        services.AddScoped<ICreateCustomer, CreateCustomerHandler>();
        services.AddScoped<IUpdateCustomer, UpdateCustomerHandler>();
        services.AddScoped<IDeleteCustomer, DeleteCustomerHandler>();
        services.AddScoped<ICustomerDetails, CustomerDetailsHandler>();
        services.AddScoped<ICustomersList, CustomersListHandler>();
        services.AddScoped<ICreateUser, CreateUserHandler>();
        services.AddScoped<IUserLogin, UserLoginHandler>();
        services.AddScoped<IUserDetailsHandler, UserDetailsHandler>();

        services.AddScoped<IValidator<UserLoginRequest>, UserLoginRequestValidator>();
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
        services.AddScoped<IValidator<CustomersListRequest>, CustomersListRequestValidator>();
        services.AddScoped<IValidator<CustomerDetailsRequest>, CustomerDetailsRequestValidator>();
        services.AddScoped<IValidator<DeleteCustomerRequest>, DeleteCustomerRequestValidator>();
        services.AddScoped<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
        return services;
    }
}
