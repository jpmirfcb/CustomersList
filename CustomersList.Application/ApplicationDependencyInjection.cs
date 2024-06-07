﻿using CustomersList.Application.Models;
using CustomersList.Application.Services.Authentication;
using CustomersList.Application.Services.Customers;
using CustomersList.Application.Services.Users;
using CustomersList.Application.Validation.Customers;
using CustomersList.Application.Validation.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersList.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication( this IServiceCollection services )
    {

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
        services.AddScoped<IValidator<CustomerListRequest>, CustomerListRequestValidator>();
        services.AddScoped<IValidator<CustomerDetailsRequest>, CustomerDetailsRequestValidator>();
        services.AddScoped<IValidator<DeleteCustomerRequest>, DeleteCustomerRequestValidator>();
        services.AddScoped<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
        return services;
    }
}
