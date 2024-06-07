using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Customers.Details;

public sealed class CustomerDetailsHandler : UseCaseQuery<CustomerDetailsRequest, CustomerDetailsResponse, CustomerDetailsRequestValidator>, ICustomerDetails
{
    private readonly ILogger<CustomerDetailsHandler> _logger;
    private readonly ICustomersRepository _customersRepository;

    public CustomerDetailsHandler( IMapper mapper, ILogger<CustomerDetailsHandler> logger, ICustomersRepository customersRepository ) : base(mapper)
    {
        _logger = logger;
        _customersRepository = customersRepository;
    }

    protected override async Task<Result<CustomerDetailsResponse>> HandleAsync( CustomerDetailsRequest request, CancellationToken ct )
    {
        try
        {
            var id = Guid.Parse(request.Id);

            var customer = await _customersRepository.GetByIdAsync(id);

            if (customer is null)
            {
                return Result<CustomerDetailsResponse>.NotFound("Customer not found.");
            }

            return Result<CustomerDetailsResponse>.Success(Mapper.Map<CustomerDetailsResponse>(customer));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the customer.");
            return Result<CustomerDetailsResponse>.Error("An error occurred while getting the customer.");
        }
    }
}
