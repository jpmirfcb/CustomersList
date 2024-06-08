using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Customers.Delete;

public sealed class DeleteCustomerHandler : UseCaseCommand<DeleteCustomerRequest, DeleteCustomerRequestValidator>, IDeleteCustomer
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<DeleteCustomerHandler> _logger;

    public DeleteCustomerHandler( IMapper mapper, ICustomersRepository customersRepository, ILogger<DeleteCustomerHandler> logger )
        : base(mapper)
    {
        _customersRepository = customersRepository;
        _logger = logger;
    }

    protected override async Task<Result> HandleAsync( DeleteCustomerRequest request, CancellationToken ct )
    {
        try
        {
            var id = Guid.Parse(request.Id);

            var exists = await _customersRepository.ExistsAsync(id);
            if (!exists)
            {
                return Result.NotFound();
            }

            await _customersRepository.DeleteAsync(id);
            return Result.NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the customer.");
            return Result.Error("An error occurred while deleting the customer.");
        }
    }
}
