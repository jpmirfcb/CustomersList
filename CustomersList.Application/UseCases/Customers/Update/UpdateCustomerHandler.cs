using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Customers.Update;

public sealed class UpdateCustomerHandler : UseCaseCommand<UpdateCustomerRequest, UpdateCustomerRequestValidator> , IUpdateCustomer
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<UpdateCustomerHandler> _logger;
    public UpdateCustomerHandler( IMapper mapper, ICustomersRepository customersRepository, ILogger<UpdateCustomerHandler> logger ) : base(mapper)
    {
        _customersRepository = customersRepository;
        _logger = logger;
    }

    protected override async Task<Result> HandleAsync( UpdateCustomerRequest request, CancellationToken ct )
    {
        try
        {
            var id = Guid.Parse(request.Id);

            if (await _customersRepository.ExistsAsync(id) == false)
            {
                return Result.NotFound();
            }

            var entity = Mapper.Map<UpdateCustomerRequest, Customer>(request);

            await _customersRepository.UpdateAsync(entity, id);

            return Result.NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the customer.");
            return Result.Error("An error occurred while updating the customer.");
        }
    }
}
