using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Customers.Create;

public sealed class CreateCustomerHandler : UseCaseQuery<CreateCustomerRequest, CreateCustomerResponse, CreateCustomerRequestValidator>, ICreateCustomer
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<CreateCustomerHandler> _logger;

    public CreateCustomerHandler( ICustomersRepository customersRepository, ILogger<CreateCustomerHandler> logger, IMapper mapper )
        : base( mapper )
    {
        _customersRepository = customersRepository;
        _logger = logger;
    }

    protected override async Task<Result<CreateCustomerResponse>> HandleAsync( CreateCustomerRequest request, CancellationToken ct )
    {
        try
        {
            var customer = Mapper.Map<Customer>(request);

            var createdCustomer = await _customersRepository.CreateAsync(customer);

            return Result<CreateCustomerResponse>.Success(Mapper.Map<CreateCustomerResponse>(createdCustomer));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the customer.");
            return Result<CreateCustomerResponse>.Error("An error occurred while creating the customer.");
        }
    }
}
